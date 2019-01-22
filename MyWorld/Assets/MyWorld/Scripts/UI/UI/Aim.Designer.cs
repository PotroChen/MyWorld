/****************************************************************************
 * 2019.1 DESKTOP-Q08CDEK
 ****************************************************************************/

namespace QFramework.Example
{
	using UnityEngine;
	using UnityEngine.UI;

	public partial class Aim
	{
		public const string NAME = "Aim";


		protected override void ClearUIComponents()
		{
			mData = null;
		}

		private AimData mPrivateData = null;

		public AimData mData
		{
			get { return mPrivateData ?? (mPrivateData = new AimData()); }
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
