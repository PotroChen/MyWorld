/****************************************************************************
 * 2019.1 DESKTOP-Q08CDEK
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public class AimData : UIPanelData
	{
		// TODO: Query Mgr's Data
	}

	public partial class Aim : UIPanel
	{
		protected override void ProcessMsg (int eventId,QMsg msg)
		{
			throw new System.NotImplementedException ();
		}

		protected override void InitUI(IUIData uiData = null)
		{
			mData = uiData as AimData ?? new AimData();
			//please add init code here
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}

		void ShowLog(string content)
		{
			Debug.Log("[ Aim:]" + content);
		}
	}
}