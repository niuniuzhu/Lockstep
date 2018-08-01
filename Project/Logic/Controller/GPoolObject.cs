using Logic.Misc;

namespace Logic.Controller
{
	public abstract class GPoolObject
	{
		protected string _rid;
		public string rid => this._rid;

		public int reference { get; private set; }

		public void AddRef( bool log = true )
		{
			++this.reference;
			if ( log )
				LLogger.Info( "[Add]{0}: {1}", this.rid, this.reference );
		}

		public void RedRef( bool log = true )
		{
			if ( log )
				LLogger.Info( "[Red]{0}: {1}", this.rid, this.reference );
			--this.reference;
		}

		public void Dispose()
		{
			this.InternalDispose();
		}

		protected abstract void InternalDispose();
	}
}