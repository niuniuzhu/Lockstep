if jit then		
	if jit.opt then
		jit.opt.start(3)			
	end
	print("jit", jit.status())
	print(string.format("os: %s, arch: %s", jit.os, jit.arch))
end

Vec2=CS.CMath.Vec2
Vec3=CS.CMath.Vec3
Vec4=CS.CMath.Vec4
Logger=CS.Client.Logic.Misc.LLogger
EntityFlag=CS.Client.Logic.Model.EntityFlag
EntityData=CS.Client.Logic.Model.EntityData
BattleData=CS.Client.Logic.Model.BattleData
Battle=CS.Client.Logic.Battle
Entity=CS.Client.Logic.Controller.Entity
Bio=CS.Client.Logic.Controller.Bio
Buff=CS.Client.Logic.Controller.Buff
UpdateHandler=CS.Client.Logic.Misc.Scheduler.UpdateHandler
TimerHandler=CS.Client.Logic.Misc.TimerEntry.TimerHandler
Test = CS.Client.Logic.Test

__dwrapper = function(func, _self) return function(...) if _self==nil then func(...) else func(_self, ...) end end end