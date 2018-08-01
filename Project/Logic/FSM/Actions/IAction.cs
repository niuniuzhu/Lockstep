namespace Logic.FSM.Actions
{
	public interface IAction
	{
		/// <summary>
		/// 该行为的状态拥有者
		/// </summary>
		FSMState state { get; set; }

		bool enable { set; get; }

		/// <summary>
		/// 进入该行为
		/// </summary>
		/// <param name="param">携带参数</param>
		void Enter( object[] param );

		/// <summary>
		/// 退出该行为
		/// </summary>
		void Exit();

		void Update( UpdateContext context );
	}
}