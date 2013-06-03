/**
 * @interface strange.extensions.command.api.ICommandBinder
 * 
 * Interface for a Binder that triggers the instantiation of Commands.
 * 
 * Commands are where the logic of your application belongs.
 * These Commands typically focus on a single function, such as
 * adding a View, requesting a service, reading from or saving to a model.
 * 
 * The act of binding events to Commands means that code needn't know
 * anything about an event recipient, or even how the event will be used.
 * For example, a Mediator might send out an event that two View objects
 * collided. A Command would then determine that the result of that event
 * was to Destroy both objects, tell a ScoreKeeper model to change the
 * score and request a message be sent to the server. Whether that
 * example means one Command or three is up to your coding preference...
 * CommandBinder can trigger one Command or multiple Commands off the
 * same event.
 * 
 * Note that Strange also a features a Sequencer. CommandBinder fires all
 * Commands in parallel, while Sequencer runs them serially, with the
 * option of suspending the chain at any time.
 * 
 * Example bindings:

		Bind("someEvent").To<SomeCommand>(); //Works, but poor form to use strings. Use the next example instead

		Bind(EventMap.SOME_EVENT).To<SomeCommand>(); //Make it a constant

		Bind(ContextEvent.START).To<StartCommand>().Once(); //Destroy the binding immediately after a single use

 * 
 * See ICommand for details on asynchronous Commands.
 */

using System;
using strange.extensions.injector.api;
using strange.framework.api;

namespace strange.extensions.command.api
{
	public interface ICommandBinder : IBinder
	{

		/// Trigger a key that unlocks one or more Commands
		void ReactTo (object trigger);

		/// Trigger a key that unlocks one or more Commands and provide a data injection to that Command
		void ReactTo (object trigger, object data);

		/// Release a previously retained Command.
		/// By default, a Command is garbage collected at the end of its `Execute()` method. 
		/// But the Command can be retained for asynchronous calls.
		void ReleaseCommand(ICommand command);

		/// Bind a trigger Key by generic Type
		new ICommandBinding Bind<T>();

		/// Bind a trigger Key by value
		new ICommandBinding Bind(object value);
	}
}

