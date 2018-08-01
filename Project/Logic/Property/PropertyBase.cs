using System.Collections.Generic;
using Core.Math;

namespace Logic.Property
{
	public abstract class PropertyBase
	{
		public delegate void AttrChangedHandler( Attr attr, object oldValue, object newValue );

		public event AttrChangedHandler OnChanged;

		protected readonly Dictionary<Attr, object> _attrMap = new Dictionary<Attr, object>();

		public object this[Attr attr] => this._attrMap[attr];

		public void Add( Attr attr, float value )
		{
			this.Add( attr, value, out _, out _ );
		}

		public void Add( Attr attr, float value, out float oldValue, out float newValue )
		{
			if ( !this._attrMap.TryGetValue( attr, out object v ) )
				v = 0f;

			oldValue = ( float )v;
			newValue = oldValue + value;

			this.Clamp( attr, ref newValue );

			if ( MathUtils.Approximately( oldValue, newValue ) )
				return;

			this._attrMap[attr] = newValue;
			this.OnChanged?.Invoke( attr, oldValue, newValue );
		}

		public void Mul( Attr attr, float value )
		{
			this.Mul( attr, value, out _, out _ );
		}

		public void Mul( Attr attr, float value, out float oldValue, out float newValue )
		{
			if ( !this._attrMap.TryGetValue( attr, out object v ) )
				v = 0f;

			oldValue = ( float )v;
			newValue = oldValue * value;

			this.Clamp( attr, ref newValue );

			if ( MathUtils.Approximately( oldValue, newValue ) )
				return;

			this._attrMap[attr] = newValue;
			this.OnChanged?.Invoke( attr, oldValue, newValue );
		}

		public void Equal( Attr attr, float value )
		{
			this.Equal( attr, value, out _, out _ );
		}

		public void Equal( Attr attr, float value, out float oldValue, out float newValue )
		{
			if ( !this._attrMap.TryGetValue( attr, out object v ) )
				v = 0f;

			oldValue = ( float )v;
			newValue = value;

			this.Clamp( attr, ref newValue );

			if ( MathUtils.Approximately( oldValue, newValue ) )
				return;

			this._attrMap[attr] = newValue;
			this.OnChanged?.Invoke( attr, oldValue, newValue );
		}

		public void Add( Attr attr, int value )
		{
			this.Add( attr, value, out _, out _ );
		}

		public void Add( Attr attr, int value, out int oldValue, out int newValue )
		{
			if ( !this._attrMap.TryGetValue( attr, out object v ) )
				v = 0;

			oldValue = ( int )v;
			newValue = oldValue + value;

			this.Clamp( attr, ref newValue );

			if ( oldValue == newValue )
				return;

			this._attrMap[attr] = newValue;
			this.OnChanged?.Invoke( attr, oldValue, newValue );
		}

		public void Mul( Attr attr, int value )
		{
			this.Mul( attr, value, out _, out _ );
		}

		public void Mul( Attr attr, int value, out int oldValue, out int newValue )
		{
			if ( !this._attrMap.TryGetValue( attr, out object v ) )
				v = 0;

			oldValue = ( int )v;
			newValue = oldValue * value;

			this.Clamp( attr, ref newValue );

			if ( oldValue == newValue )
				return;

			this._attrMap[attr] = newValue;
			this.OnChanged?.Invoke( attr, oldValue, newValue );
		}

		public void Equal( Attr attr, int value )
		{
			this.Equal( attr, value, out _, out _ );
		}

		public void Equal( Attr attr, int value, out int oldValue, out int newValue )
		{
			if ( !this._attrMap.TryGetValue( attr, out object v ) )
				v = 0;

			oldValue = ( int )v;
			newValue = value;

			this.Clamp( attr, ref newValue );

			if ( oldValue == newValue )
				return;

			this._attrMap[attr] = newValue;
			this.OnChanged?.Invoke( attr, oldValue, newValue );
		}

		public void Equal( Attr attr, string value )
		{
			this.Equal( attr, value, out _, out _ );
		}

		public void Equal( Attr attr, string value, out string oldValue, out string newValue )
		{
			if ( !this._attrMap.TryGetValue( attr, out object v ) )
				v = string.Empty;

			oldValue = ( string )v;
			newValue = value;

			if ( oldValue == newValue )
				return;

			this._attrMap[attr] = newValue;
			this.OnChanged?.Invoke( attr, oldValue, newValue );
		}

		public void Equal( Attr attr, object value )
		{
			this.Equal( attr, value, out _ );
		}

		public void Equal( Attr attr, object value, out object oldValue )
		{
			this._attrMap.TryGetValue( attr, out oldValue );

			if ( Equals( value, oldValue ) )
				return;

			this._attrMap[attr] = value;
			this.OnChanged?.Invoke( attr, oldValue, value );
		}

		private void Clamp( Attr attr, ref float value )
		{
			switch ( attr )
			{
				case Attr.Cooldown:
					value = value < 0f ? 0f : value;
					break;
				case Attr.Hp:
					value = MathUtils.Clamp( value, 0, ( float )this._attrMap[Attr.Mhp] );
					break;
				case Attr.Mana:
					value = MathUtils.Clamp( value, 0, ( float )this._attrMap[Attr.Mmana] );
					break;
			}
		}

		private void Clamp( Attr attr, ref int value )
		{
		}
	}
}