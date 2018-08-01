using System.Collections.Generic;

namespace Logic.Misc
{
	public abstract class AbsScheduler<T>
	{
		private uint _gid;

		protected readonly List<T> _objs = new List<T>();
		private readonly HashSet<T> _toRemoves = new HashSet<T>();
		private readonly Dictionary<uint, T> _idToObjs = new Dictionary<uint, T>();

		private uint GetGid()
		{
			if ( ++this._gid >= uint.MaxValue )
			{
				LLogger.Warning( "Reset gid of scheduler" );
				this._gid = 0;
			}
			return this._gid;
		}

		public uint Register( T obj )
		{
			uint gid = this.GetGid();
			this._idToObjs[gid] = obj;
			this._objs.Add( obj );
			return gid;
		}

		public void Unregister( T obj )
		{
			if ( !this._objs.Contains( obj ) )
				return;
			this._toRemoves.Add( obj );
			this.OnUnregister( obj );
		}

		public void Unregister( uint id )
		{
			if ( !this._idToObjs.ContainsKey( id ) )
				return;
			this.Unregister( this._idToObjs[id] );
		}

		public bool Contains( T obj )
		{
			return this._objs.Contains( obj );
		}

		protected virtual void OnUnregister( T obj )
		{
		}

		internal void Dispose()
		{
			foreach ( T obj in this._toRemoves )
				this._objs.Remove( obj );
			this._toRemoves.Clear();
		}

		internal virtual void Update( float dt )
		{

		}
	}

	public delegate void UpdateHandler( float dt );

	public class FrameScheduler : AbsScheduler<UpdateHandler>
	{
		internal override void Update( float dt )
		{
			base.Update( dt );
			int count = this._objs.Count;
			for ( int i = 0; i < count; i++ )
				this._objs[i].Invoke( dt );
		}
	}

	public class TimeScheduler : AbsScheduler<TimerEntry>
	{
		public uint Register( float interval, int repeat, bool startImmediately, TimerEntry.TimerHandler timerCallback,
							  TimerEntry.CompleteHandler completeCallback, object param = null )
		{
			TimerEntry entry = new TimerEntry( interval, repeat, startImmediately, timerCallback, completeCallback, param );
			return this.Register( entry );
		}

		public void Unregister( TimerEntry.TimerHandler callback )
		{
			int count = this._objs.Count;
			for ( int i = 0; i < count; i++ )
			{
				TimerEntry timerEntry = this._objs[i];
				if ( timerEntry.timerCallback == callback )
					this.Unregister( timerEntry );
			}
		}

		protected override void OnUnregister( TimerEntry entry )
		{
			entry.Dispose();
		}

		internal override void Update( float dt )
		{
			base.Update( dt );
			int count = this._objs.Count;
			for ( int i = 0; i < count; i++ )
			{
				TimerEntry timerEntry = this._objs[i];
				timerEntry.OnUpdate( dt );
				if ( timerEntry.finished )
					this.Unregister( timerEntry );
			}
		}
	}

	public class Scheduler : AbsScheduler<ScheduleEntry>
	{
		public uint Register( float[] times, ScheduleEntry.ScheduleHandler timerCallback,
									  ScheduleEntry.CompleteHandler completeCallback, object param = null )
		{
			ScheduleEntry entry = new ScheduleEntry( times, timerCallback, completeCallback, param );
			return this.Register( entry );
		}

		public void Unregister( ScheduleEntry.ScheduleHandler callback )
		{
			int count = this._objs.Count;
			for ( int i = 0; i < count; i++ )
			{
				ScheduleEntry scheduleEntry = this._objs[i];
				if ( scheduleEntry.scheduleCallback == callback )
					this.Unregister( scheduleEntry );
			}
		}

		protected override void OnUnregister( ScheduleEntry entry )
		{
			entry.Dispose();
		}

		internal override void Update( float dt )
		{
			base.Update( dt );
			int count = this._objs.Count;
			for ( int i = 0; i < count; i++ )
			{
				ScheduleEntry scheduleEntry = this._objs[i];
				scheduleEntry.OnUpdate( dt );
				if ( scheduleEntry.finished )
					this.Unregister( scheduleEntry );
			}
		}
	}
}