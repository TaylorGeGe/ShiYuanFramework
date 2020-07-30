using NUnit.Framework;

namespace Sy.Tests
{
    public class FSMTests
    {
        [Test]
        public void FSM_Test()
        {
            //        Idle,               闲置
            //        Run,                跑
            //        Jump,               一段跳
            //        DoubleJump,         二段跳
            //        Die,                挂彩
            var fsm = new FSM();

            // 创建状态
            var idleState = new FSM.FSMState("idle");
            var runState = new FSM.FSMState("run");
            var jumpState = new FSM.FSMState("jump");
            var doubleJumpState = new FSM.FSMState("double_jump");
            var dieState = new FSM.FSMState("die");

            // 创建跳转
            var jumpCalled = false;
            var touchTranslation1 =
                new FSM.FSMTranslation(runState, "touch_down", jumpState, () => { jumpCalled = true; });

            var doubleJumpCalled = false;
            var touchTranslation2 = new FSM.FSMTranslation(jumpState, "touch_down", doubleJumpState,
                () => { doubleJumpCalled = true; });

            var runCalledCount = 0;
            var landTranslation1 = new FSM.FSMTranslation(jumpState, "land", runState, () => { runCalledCount++; });

            var landTranslation2 =
                new FSM.FSMTranslation(doubleJumpState, "land", runState, () => { runCalledCount++; });

            // 添加状态
            fsm.AddState(idleState);
            fsm.AddState(runState);
            fsm.AddState(jumpState);
            fsm.AddState(doubleJumpState);
            fsm.AddState(dieState);

            // 添加跳转
            fsm.AddTranslation(touchTranslation1);
            fsm.AddTranslation(touchTranslation2);
            fsm.AddTranslation(landTranslation1);
            fsm.AddTranslation(landTranslation2);

            // 初识状态是 runState
            fsm.Start(runState);

            Assert.AreSame(fsm.CurState, runState);

            // 点击屏幕，进行跳跃
            fsm.HandleEvent("touch_down");

            Assert.IsTrue(jumpCalled);
            Assert.AreSame(fsm.CurState, jumpState);

            // 点击屏幕，二段跳
            fsm.HandleEvent("touch_down");

            Assert.IsTrue(doubleJumpCalled);
            Assert.AreSame(fsm.CurState, doubleJumpState);

            // 着陆
            fsm.HandleEvent("land");

            Assert.AreEqual(runCalledCount, 1);
            Assert.AreSame(fsm.CurState, runState);
        }
    }
}