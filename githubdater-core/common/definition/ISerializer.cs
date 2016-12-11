namespace NGitHubdater
{
    /// <summary>
    /// Represents a simple serializer
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Load an object from the provided path.
        /// </summary>
        /// <typeparam name="T">The type of the object to load.</typeparam>
        /// <param name="path">The path to load the object from.</param>
        /// <returns>The loaded object.</returns>
        T Load<T>(string path);

        /// <summary>
        /// Save an object to the provided path.
        /// </summary>
        /// <typeparam name="T">The type of the object to save.</typeparam>
        /// <param name="path">The path where the object will be saved.</param>
        /// <param name="obj">The object to save.</param>
        void Save<T>(string path, T obj);
    }
}
