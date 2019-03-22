using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    [PublicAPI]
    public interface IServiceLocator
    {
        /// <summary>
        /// <para>Attempts to locate given <paramref name="application"/> in given <paramref name="environment"/>.</para>
        /// <para>Returns an instance of <see cref="IServiceTopology"/> if application is successfully located.</para>
        /// <para>Returns <c>null</c> in following cases:</para>
        /// <list type="bullet">
        ///     <item><description>No such environment exists.</description></item>
        ///     <item><description>No such service exists in given environment.</description></item>
        ///     <item><description>An error occurs while accessing service discovery registry.</description></item>
        /// </list>
        /// <para>This method is expected to be thread-safe.</para>
        /// <para>This method is not expected to throw exceptions (<c>null</c> should be returned instead).</para>
        /// </summary>
        /// <param name="environment">Name of the environment to search in.</param>
        /// <param name="application">Name of the application to search for.</param>
        [CanBeNull]
        IServiceTopology Locate([NotNull] string environment, [NotNull] string application);
    }
}