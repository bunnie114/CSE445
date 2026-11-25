using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace DefaultPage
{
    public class UserManager
    {
        /// <summary>
        /// Private helper method to get the user objects from xml.
        /// </summary>
        /// <param name="filePath">The file path to update</param>
        /// <returns>The XML as a list of user objects</returns>
        private List<User> getUsers(string filePath)
        {
            // If file does not yet exist, return an empty user list
            if (!File.Exists(filePath))
            {
                return new List<User>();
            }

            // Create an XmlSerializer to read the xml into a list of User objects
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));

            // Read the xml file and deserialize it into a list of User objects
            using (StreamReader reader = new StreamReader(filePath))
            {
                List<User> users = (List<User>)serializer.Deserialize(reader);
                return users;
            }
        }

        /// <summary>
        /// Method to find the user object in the xml based on the username.
        /// </summary>
        /// <param name="filePath">The file path to update</param>
        /// <param name="username">The username to search for</param>
        /// <returns>The user object if found, otherwise null</returns>
        public User findUser(string filePath, string username)
        {
            // Get the list of users
            List<User> users = getUsers(filePath);

            // Check if the username exists in the list of users
            foreach (var u in users)
            {
                if (u.Username.ToLower() == username.ToLower())
                {
                    return u;
                }
            }

            return null;
        }

        /// <summary>
        /// Method to get check if the email is taken.
        /// </summary>
        /// <param name="email">The email to check</param>
        /// <returns>The true if email exists and false otherwise</returns>
        public bool emailExists(string filePath, string email)
        {
            // Get the list of users
            List<User> users = getUsers(filePath);

            // Check if the email exists in the list of users
            foreach (var u in users)
            {
                if (u.Email.ToLower() == email.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Method to add a new user to the XML.
        /// </summary>
        /// <param name="filePath">The file path to update</param>
        /// <param name="user">The user to store in the xml</param>
        public void addUser(string filePath, User user)
        {
            // Get the list of users and add new user to end of list
            List<User> users = getUsers(filePath);
            users.Add(user);

            // Create an XmlSerializer to write the user object into the xml
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));

            // Write the user object to the xml file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, users);
            }
        }
    }
}