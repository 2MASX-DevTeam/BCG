using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace Tools
{
	/// <summary>
	/// Summary description for ViewStateViewer.
	/// </summary>
	public class ViewStateViewer
	{
		public ViewStateViewer()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public void GetPageControlsViewState()
		{
			System.Web.UI.HtmlControls.HtmlGenericControl  VSHtml = new  System.Web.UI.HtmlControls.HtmlGenericControl();

			if ( ((Page)HttpContext.Current.Handler).Request.Form["__VIEWSTATE"] != null)
			{
				string vsInput = ((Page)HttpContext.Current.Handler).Request.Form["__VIEWSTATE"].Replace("\n", "").Replace("\r", "");
				object vsObject = new LosFormatter().Deserialize(vsInput);				  
				// -Triplet.First
				// Get Page view state - the First object in the first Triplet
				VSHtml.InnerHtml = "<b>Page Data : </b><br>" +  ParseViewState(((Triplet)vsObject).First,1);
				// -Triplet.First - Page hash value
				// -Triplet.Second
				//               .First(Pair)
				//                          .First(ArrayList) - keys
				//                          .Second(ArrayList)- values
				// get the secound object of the first triplet, if the first object of that object is pair
				// it holds user viewstate. the first element in the pair is arraylist of keys and the second element in the pair 
				// is the arraylist of values.
				Triplet Second = (Triplet)((Triplet)vsObject).Second;
				if (Second.First is System.Web.UI.Pair)
				{
					System.Web.UI.Pair oPair = (System.Web.UI.Pair)Second.First;
					System.Collections.ArrayList oKeys = (System.Collections.ArrayList)oPair.First;  
					System.Collections.ArrayList oVals = (System.Collections.ArrayList)oPair.Second;
					VSHtml.InnerHtml += "<br><b> User ViewState : </b><BR>";
					for(int i = 0; i<oKeys.Count; i++)
					{
						VSHtml.InnerHtml += "\t Key=" + oKeys[i].ToString () + " Value=" + oVals[i].ToString () + "<br>";
					}					
				}
				// -Triplet.First - Page hash value
				// -Triplet.Second
				//               .First(Pair)
				//                          .First(ArrayList) - keys
				//                          .Second(ArrayList)- values
				//               .Third(ArrayList)
				//                                [X](Triplet)
				//                                            .Second(ArrayList) - array that holds the control
				//                                                                 Position in Form controls
				//                                                                 collection.
				//                                            .Third(ArrayList) - array that holds the viewstate
				//                                                                for every control from the upper
				//                                                                array. 
				// get object list. first we get the third object that contains array of triplets
				ArrayList oArrObjectsAndData = (ArrayList)Second.Third;
				// loop thrhogou the array triplet 
				for (int iObjAndData=0; iObjAndData < oArrObjectsAndData.Count ; iObjAndData++)
				{
					// get the triplet second and third objects that contain list of control location in the form
					// controls collection and the match entry in the third object contain the control ViewState.
					Triplet oTripControls = (Triplet)oArrObjectsAndData[iObjAndData];
					ArrayList oArrObjects = (ArrayList)oTripControls.Second;
					ArrayList oArrData = (ArrayList)oTripControls.Third;				
					for(int iCont =0; iCont < oArrObjects.Count; iCont++) 
					{
						// get the control ID
						string ContID = GetForm().Controls[(int)oArrObjects[iCont]].ID;
						// Get the control ViewState.
						Triplet oTrip = (Triplet)oArrData[iCont];
						VSHtml.InnerHtml += "<br><b>" + ContID + " : </b><BR>" + ParseViewState(oTrip ,5);
					}										
				}
				VSHtml.Visible = true;
				GetForm().Controls.Add (VSHtml); 				
			}
		}
		private Control GetForm()
		{
			System.Collections.IEnumerator oEnum = ((Page)HttpContext.Current.Handler).Controls.GetEnumerator ();   
			while(oEnum.MoveNext())
			{
				if (oEnum.Current is System.Web.UI.HtmlControls.HtmlForm )
				{
					return ((System.Web.UI.HtmlControls.HtmlForm )oEnum.Current);
				}
			}
			return null;
		}

		
		
		private string tabs(int Level)
		{
			string tabs = "";
			for (int i =0;i < Level; i++)
			{
				tabs += "-";
			}
			return tabs;
		}


		private string ParseViewState(object ViewState, int Level) 
		{
			  
			if (ViewState == null) 
			{
				
				return ItemViewState(tabs(Level-1) , "null");
			}
			else if (ViewState.GetType() == typeof(System.Web.UI.Triplet)) 
			{
				string children = ParseViewState((Triplet) ViewState, Level);
				
				return NodeViewState(tabs(Level-1) + "Triplet", tabs(Level-1) + children);
			}
			else if (ViewState.GetType() == typeof(System.Web.UI.Pair)) 
			{
				string children = ParseViewState((Pair) ViewState, Level);				
				return NodeViewState(tabs(Level-1) + "Pair", tabs(Level-1) + children);
			}
			else if (ViewState.GetType() == typeof(System.Collections.ArrayList)) 
			{
				string children = ParseViewState((IEnumerable) ViewState, Level);
				
				return NodeViewState(tabs(Level-1) + "ArrayList", tabs(Level-1) + children);
			}
			else if (ViewState.GetType().IsArray) 
			{
				string children = ParseViewState((IEnumerable) ViewState, Level);				
				return NodeViewState(tabs(Level-1) +"Array", tabs(Level-1)+ children);
			}
			else if (ViewState.GetType() == typeof(System.String)) 
			{				
				return ItemViewState(tabs(Level-1) , ViewState.ToString());
			}
			else if (ViewState.GetType().IsPrimitive) 
			{				
				return ItemViewState(tabs(Level-1) , ViewState.ToString());
			}
			else 
			{				
				return ItemViewState(tabs(Level-1) , ViewState.GetType().ToString());
			}
		}

		private string ParseViewState(Triplet ViewState, int Level) 
		{
			string first = ParseViewState(ViewState.First, Level + 1);
			string second = ParseViewState(ViewState.Second, Level + 1);
			string third = ParseViewState(ViewState.Third, Level + 1);
			
			return tabs(Level-1) + first + second + third;
		}

		private string ParseViewState(Pair ViewState, int Level) 
		{
			string first = ParseViewState(ViewState.First, Level + 1);
			string second = ParseViewState(ViewState.Second, Level + 1);			
			return tabs(Level-1) + first + second;
		}

		private string ParseViewState(IEnumerable ViewState, int Level) 
		{
			string items = "";
			foreach (object item in ViewState) 
			{
				items += ParseViewState(item, Level + 1);
			}
			
			return tabs(Level-1) + items;
		}
		private string NodeViewState(string Item, string Children) 
		{
			return
				@"<div>" +  Item + @"<div style='display: block;'>" +
				Children + @"</div></div>";
		}

		private string ItemViewState(string Item) 
		{
			return "<div>" + HttpContext.Current.Server.HtmlEncode(Item) + "</div>";
		}
		private string ItemViewState(string tabs, string Item) 
		{
			return "<div>" + tabs + ((Page)HttpContext.Current.Handler).Server.HtmlEncode(Item) + "</div>";
		}

	}
}
