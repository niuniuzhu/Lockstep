using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using UnityEngine;
using WorldEditor.Attributes;
using WorldEditor.Forms;
using ValueType = WorldEditor.Attributes.ValueType;

namespace WorldEditor
{
	public class ItemView
	{
		public delegate void ItemSelectedEvent();
		public event ItemSelectedEvent ItemSelected;

		private readonly ListView _listView;
		private readonly Dictionary<ListViewItem, PropertyInfo> _listViewItemToNode = new Dictionary<ListViewItem, PropertyInfo>();
		private readonly Dictionary<PropertyInfo, ListViewItem> _nodeToListViewItem = new Dictionary<PropertyInfo, ListViewItem>();

		public PropertyInfo selected
		{
			get
			{
				if ( this._listView.SelectedItems.Count == 0 )
					return null;
				ListViewItem listViewItem = this._listView.SelectedItems[this._listView.SelectedItems.Count - 1];
				return this._listViewItemToNode[listViewItem];
			}
		}

		public ItemView( ListView listView )
		{
			this._listView = listView;
		}

		private void OnIndexChanged( object sender, EventArgs e )
		{
			this.OnIndexChanged();
		}

		private void OnIndexChanged()
		{
			if ( this.ItemSelected != null )
				this.ItemSelected.Invoke();
		}

		private void listView_KeyDown( object sender, KeyEventArgs e )
		{
			if ( e.KeyCode == Keys.Enter )
			{
				//DataCenter.currNode = this.selected;
				this.PopupEditor();
			}
		}

		private void listView_DoubleClick( object sender, EventArgs e )
		{
			//DataCenter.currNode = this.selected;
			this.PopupEditor();
		}

		public void Refresh()
		{
			//this._listView.SelectedIndexChanged -= this.OnIndexChanged;
			//this._listView.DoubleClick -= this.listView_DoubleClick;
			//this._listView.KeyDown -= this.listView_KeyDown;

			//this._listView.BeginUpdate();

			//this._listView.Items.Clear();
			//this._listViewItemToNode.Clear();
			//this._nodeToListViewItem.Clear();

			//if ( DataCenter.minorNode != null )
			//{
			//	System.Reflection.PropertyInfo[] pis = DataCenter.currNode.GetType().GetProperties();
			//	foreach ( System.Reflection.PropertyInfo pi in pis )
			//	{
			//		DataPropertyAttribute attribute = pi.GetCustomAttribute<DataPropertyAttribute>();
			//		if ( attribute == null )
			//			continue;

			//		ListViewItem listViewItem = new ListViewItem();
			//		listViewItem.Text = attribute.name;
			//		listViewItem.SubItems.Add( pi.GetValue( DataCenter.minorNode, null ).ToString() );

			//		PropertyInfo propertyInfo = new PropertyInfo( string.Empty, pi.Name );
			//		propertyInfo.propertyInfo = pi;

			//		this._listView.Items.Add( listViewItem );
			//		this._listViewItemToNode[listViewItem] = propertyInfo;
			//		this._nodeToListViewItem[propertyInfo] = listViewItem;
			//	}
			//}

			//this._listView.EndUpdate();

			//this._listView.SelectedIndexChanged += this.OnIndexChanged;
			//this._listView.DoubleClick += this.listView_DoubleClick;
			//this._listView.KeyDown += this.listView_KeyDown;
			//this._listView.Items[0].Selected = true;
		}

		private void PopupEditor()
		{
		//	if ( this._listView.SelectedItems.Count == 0 )
		//		return;

		//	System.Reflection.PropertyInfo pi = this.selected.propertyInfo;
		//	DataPropertyAttribute attribute = pi.GetCustomAttribute<DataPropertyAttribute>();
		//	object value = pi.GetValue( DataCenter.minorNode, null );

		//	ValueEditor editor = null;
		//	switch ( attribute.valueType )
		//	{
		//		case ValueType.String:
		//			{
		//				StringEditor stringEditor = new StringEditor();
		//				stringEditor.value = value.ToString();
		//				editor = stringEditor;
		//			}
		//			break;

		//		case ValueType.Boolean:
		//			{
		//				BooleanEditor booleanEditor = new BooleanEditor();
		//				booleanEditor.value = Convert.ToBoolean( value );
		//				editor = booleanEditor;
		//			}
		//			break;

		//		case ValueType.Int:
		//		case ValueType.Float:
		//		case ValueType.Long:
		//		case ValueType.Double:
		//			{
		//				NumberEditor numberEditor = new NumberEditor();
		//				numberEditor.valueType = attribute.valueType;
		//				numberEditor.step = attribute.valueType == ValueType.Int ? 1 : attribute.step;
		//				numberEditor.min = attribute.min;
		//				numberEditor.max = attribute.max;
		//				numberEditor.decimalPlaces = attribute.decimalPlaces;
		//				numberEditor.value = Convert.ToDecimal( value );
		//				editor = numberEditor;
		//			}
		//			break;

		//		case ValueType.Vector2:
		//			{
		//				VectorEditor vectorEditor = new VectorEditor();
		//				vectorEditor.step = attribute.step;
		//				vectorEditor.min = attribute.min;
		//				vectorEditor.max = attribute.max;
		//				vectorEditor.decimalPlaces = attribute.decimalPlaces;
		//				vectorEditor.vector2 = ( Vector2 )value;
		//				editor = vectorEditor;
		//			}
		//			break;

		//		case ValueType.Vector3:
		//			{
		//				VectorEditor vectorEditor = new VectorEditor();
		//				vectorEditor.step = attribute.step;
		//				vectorEditor.min = attribute.min;
		//				vectorEditor.max = attribute.max;
		//				vectorEditor.decimalPlaces = attribute.decimalPlaces;
		//				vectorEditor.vector3 = ( Vector3 )value;
		//				editor = vectorEditor;
		//			}
		//			break;

		//		case ValueType.Vector4:
		//			{
		//				VectorEditor vectorEditor = new VectorEditor();
		//				vectorEditor.step = attribute.step;
		//				vectorEditor.min = attribute.min;
		//				vectorEditor.max = attribute.max;
		//				vectorEditor.decimalPlaces = attribute.decimalPlaces;
		//				vectorEditor.vector4 = ( Vector4 )value;
		//				editor = vectorEditor;
		//			}
		//			break;
		//	}

		//	if ( editor == null )
		//		return;

		//	editor.name = attribute.name + ":";
		//	editor.desc = attribute.desc;

		//	DialogResult dialogResult = editor.ShowDialog( Program.form );
		//	if ( dialogResult == DialogResult.OK || dialogResult == DialogResult.Cancel )
		//	{
		//		if ( dialogResult == DialogResult.OK )
		//		{
		//			pi.SetValue( DataCenter.minorNode, editor.result );
		//			ListViewItem listViewItem = this._nodeToListViewItem[this.selected];
		//			listViewItem.SubItems[1].Text = editor.result.ToString();
		//		}
		//		editor.Dispose();
		//	}
		}
	}
}