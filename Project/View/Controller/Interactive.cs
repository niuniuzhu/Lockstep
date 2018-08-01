using Logic.Controller;
using UnityEngine;

namespace View.Controller
{
	public abstract class Interactive : GPoolObject, IInteractive
	{
		public Collider collider { get; protected set; }

		protected override void InternalDispose()
		{
			this.collider = null;
		}
	}
}