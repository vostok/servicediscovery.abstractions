using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    /// <summary>
    /// <para>An instance of <see cref="IServiceBeacon"/> is responsible for registering the application in service discovery registry.</para>
    /// <para>This is a continuous process: registration is maintained until explicitly stopped (or until application terminates).</para>
    /// <para>Use <see cref="Start"/> method to register and maintain the registration in the background..</para>
    /// <para>Use <see cref="Stop"/> method to unregister.</para>
    /// </summary>
    [PublicAPI]
    public interface IServiceBeacon
    {
        /// <summary>
        /// <para>Starts the process of maintaining the registration in service discovery system.</para>
        /// <para>Registration is performed asynchronously: it may occur after <see cref="Start"/> method returns.</para>
        /// <para>This method is expected to be thread-safe.</para>
        /// <para>This method is expected to be idempotent: calls on already started beacon should have no effect.</para>
        /// </summary>
        void Start();

        /// <summary>
        /// <para>Stops the process of maintaining the registration in service discovery system.</para>
        /// <para>It is also expected for <see cref="Stop"/> to unregister synchronously (before the method returns).</para>
        /// <para>This method is expected to be thread-safe.</para>
        /// <para>This method is expected to be idempotent: calls on already stopped beacon should have no effect.</para>
        /// </summary>
        void Stop();
    }
}