// version: 1
using System.Collections.Generic;
using System;
public static class GenUniRxTypes
{
    [XLua.LuaCallCSharp] public static IEnumerable<Type> LuaCallCSharp { get { return List; } }
    [XLua.CSharpCallLua] public static List<Type> CSharpCallLua { get { return GenConfig.CSharpCallLua(List); } }
    private static System.Type[] List = {

typeof(UniRx.AnimationCurveReactiveProperty),
typeof(UniRx.AsyncMessageBroker),
typeof(UniRx.AsyncOperationExtensions),
typeof(UniRx.AsyncReactiveCommand),
typeof(UniRx.AsyncReactiveCommand<>),
typeof(UniRx.AsyncReactiveCommandExtensions),
typeof(UniRx.AsyncSubject<>),
typeof(UniRx.BehaviorSubject<>),
typeof(UniRx.BooleanDisposable),
typeof(UniRx.BooleanNotifier),
typeof(UniRx.BoolReactiveProperty),
typeof(UniRx.BoundsReactiveProperty),
typeof(UniRx.ByteReactiveProperty),
typeof(UniRx.CancellationDisposable),
typeof(UniRx.CollectionAddEvent<>),
typeof(UniRx.CollectionMoveEvent<>),
typeof(UniRx.CollectionRemoveEvent<>),
typeof(UniRx.CollectionReplaceEvent<>),
typeof(UniRx.ColorReactiveProperty),
typeof(UniRx.CompositeDisposable),
typeof(UniRx.CoroutineAsyncBridge),
typeof(UniRx.CoroutineAsyncBridge<>),
typeof(UniRx.CoroutineAsyncExtensions),
typeof(UniRx.CountNotifier),
typeof(UniRx.Diagnostics.LogEntry),
typeof(UniRx.Diagnostics.LogEntryExtensions),
typeof(UniRx.Diagnostics.Logger),
typeof(UniRx.Diagnostics.ObservableDebugExtensions),
typeof(UniRx.Diagnostics.ObservableLogger),
typeof(UniRx.Diagnostics.UnityDebugSink),
typeof(UniRx.DictionaryAddEvent<,>),
typeof(UniRx.DictionaryDisposable<,>),
typeof(UniRx.DictionaryRemoveEvent<,>),
typeof(UniRx.DictionaryReplaceEvent<,>),
typeof(UniRx.Disposable),
// typeof(UniRx.DisposableExtensions),
typeof(UniRx.DoubleReactiveProperty),
typeof(UniRx.EventPattern<>),
typeof(UniRx.EventPattern<,>),
typeof(UniRx.FloatReactiveProperty),
typeof(UniRx.FrameCountTypeExtensions),
typeof(UniRx.FrameInterval<>),
// typeof(UniRx.InspectorDisplayAttribute),
// typeof(UniRx.InspectorDisplayDrawer),
// typeof(UniRx.InternalUtil.DisposedObserver<>),
// typeof(UniRx.InternalUtil.EmptyObserver<>),
// typeof(UniRx.InternalUtil.ImmutableList<>),
// typeof(UniRx.InternalUtil.ListObserver<>),
// typeof(UniRx.InternalUtil.MicroCoroutine),
// typeof(UniRx.InternalUtil.ThreadSafeQueueWorker),
// typeof(UniRx.InternalUtil.ThrowObserver<>),
typeof(UniRx.IntReactiveProperty),
typeof(UniRx.LongReactiveProperty),
typeof(UniRx.MainThreadDispatcher),
typeof(UniRx.MessageBroker),
// typeof(UniRx.MultilineReactivePropertyAttribute),
typeof(UniRx.MultipleAssignmentDisposable),
typeof(UniRx.Notification),
typeof(UniRx.Notification<>),
typeof(UniRx.Observable),
typeof(UniRx.ObservableExtensions),
typeof(UniRx.ObservableWWW),
typeof(UniRx.ObservableYieldInstruction<>),
typeof(UniRx.ObserveExtensions),
typeof(UniRx.Observer),
typeof(UniRx.ObserverExtensions),
typeof(UniRx.Operators.OperatorObservableBase<>),
typeof(UniRx.Operators.OperatorObserverBase<,>),
typeof(UniRx.OptimizedObservableExtensions),
typeof(UniRx.Pair<>),
typeof(UniRx.QuaternionReactiveProperty),
// typeof(UniRx.RangeReactivePropertyAttribute),
typeof(UniRx.ReactiveCollection<>),
typeof(UniRx.ReactiveCollectionExtensions),
typeof(UniRx.ReactiveCommand),
typeof(UniRx.ReactiveCommand<>),
typeof(UniRx.ReactiveCommandExtensions),
typeof(UniRx.ReactiveDictionary<,>),
typeof(UniRx.ReactiveDictionaryExtensions),
typeof(UniRx.ReactiveProperty<>),
typeof(UniRx.ReactivePropertyExtensions),
typeof(UniRx.ReadOnlyReactiveProperty<>),
typeof(UniRx.RectReactiveProperty),
typeof(UniRx.RefCountDisposable),
typeof(UniRx.ReplaySubject<>),
typeof(UniRx.ScenePlaybackDetector),
typeof(UniRx.ScheduledDisposable),
typeof(UniRx.ScheduledNotifier<>),
typeof(UniRx.Scheduler),
typeof(UniRx.Scheduler.DefaultSchedulers),
typeof(UniRx.SerialDisposable),
typeof(UniRx.SingleAssignmentDisposable),
typeof(UniRx.StableCompositeDisposable),
typeof(UniRx.StringReactiveProperty),
typeof(UniRx.Subject<>),
typeof(UniRx.SubjectExtensions),
typeof(UniRx.TaskObservableExtensions),
typeof(UniRx.TimeInterval<>),
typeof(UniRx.Timestamped),
typeof(UniRx.Timestamped<>),
typeof(UniRx.Toolkit.AsyncObjectPool<>),
typeof(UniRx.Toolkit.ObjectPool<>),
typeof(UniRx.Triggers.ObservableAnimatorTrigger),
typeof(UniRx.Triggers.ObservableBeginDragTrigger),
typeof(UniRx.Triggers.ObservableCancelTrigger),
typeof(UniRx.Triggers.ObservableCanvasGroupChangedTrigger),
typeof(UniRx.Triggers.ObservableCollision2DTrigger),
typeof(UniRx.Triggers.ObservableCollisionTrigger),
typeof(UniRx.Triggers.ObservableDeselectTrigger),
typeof(UniRx.Triggers.ObservableDestroyTrigger),
typeof(UniRx.Triggers.ObservableDragTrigger),
typeof(UniRx.Triggers.ObservableDropTrigger),
typeof(UniRx.Triggers.ObservableEnableTrigger),
typeof(UniRx.Triggers.ObservableEndDragTrigger),
typeof(UniRx.Triggers.ObservableEventTrigger),
typeof(UniRx.Triggers.ObservableFixedUpdateTrigger),
typeof(UniRx.Triggers.ObservableInitializePotentialDragTrigger),
typeof(UniRx.Triggers.ObservableJointTrigger),
typeof(UniRx.Triggers.ObservableLateUpdateTrigger),
typeof(UniRx.Triggers.ObservableMouseTrigger),
typeof(UniRx.Triggers.ObservableMoveTrigger),
typeof(UniRx.Triggers.ObservableParticleTrigger),
typeof(UniRx.Triggers.ObservablePointerClickTrigger),
typeof(UniRx.Triggers.ObservablePointerDownTrigger),
typeof(UniRx.Triggers.ObservablePointerEnterTrigger),
typeof(UniRx.Triggers.ObservablePointerExitTrigger),
typeof(UniRx.Triggers.ObservablePointerUpTrigger),
typeof(UniRx.Triggers.ObservableRectTransformTrigger),
typeof(UniRx.Triggers.ObservableScrollTrigger),
typeof(UniRx.Triggers.ObservableSelectTrigger),
typeof(UniRx.Triggers.ObservableStateMachineTrigger),
typeof(UniRx.Triggers.ObservableStateMachineTrigger.OnStateInfo),
typeof(UniRx.Triggers.ObservableStateMachineTrigger.OnStateMachineInfo),
typeof(UniRx.Triggers.ObservableSubmitTrigger),
typeof(UniRx.Triggers.ObservableTransformChangedTrigger),
typeof(UniRx.Triggers.ObservableTrigger2DTrigger),
typeof(UniRx.Triggers.ObservableTriggerBase),
typeof(UniRx.Triggers.ObservableTriggerExtensions),
typeof(UniRx.Triggers.ObservableTriggerTrigger),
typeof(UniRx.Triggers.ObservableUpdateSelectedTrigger),
typeof(UniRx.Triggers.ObservableUpdateTrigger),
typeof(UniRx.Triggers.ObservableVisibleTrigger),
typeof(UniRx.Unit),
typeof(UniRx.UnityEventExtensions),
typeof(UniRx.UnityGraphicExtensions),
typeof(UniRx.UnityUIComponentExtensions),
typeof(UniRx.Vector2ReactiveProperty),
typeof(UniRx.Vector3ReactiveProperty),
typeof(UniRx.Vector4ReactiveProperty),
typeof(UniRx.WebRequestExtensions),
typeof(UniRx.WWWErrorException),

    };
}