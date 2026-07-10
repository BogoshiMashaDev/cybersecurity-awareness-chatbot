using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSecurityBotPart2
{
    public class TaskService
    {
        private readonly string connectionString =
            "server=localhost;port=3306;database=cybersecuritybot;uid=root;pwd=@Labs2026!;";

        // Add task with 3 arguments
        public string AddTask(string title, string description, DateTime? reminderDate)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"INSERT INTO Tasks
                                    (Title, Description, ReminderDate, IsCompleted)
                                    VALUES
                                    (@title,@description,@reminderDate,0)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@reminderDate",
                        reminderDate.HasValue ? reminderDate.Value : DBNull.Value);

                    cmd.ExecuteNonQuery();
                }

                return "Task added successfully.";
            }
            catch (Exception ex)
            {
                return "The task could not be saved to MySQL. " + ex.Message;
            }
        }

        // View tasks
        public string ViewTasks()
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM Tasks";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sb.AppendLine(
                                $"ID: {reader["Id"]} | {reader["Title"]} | Completed: {reader["IsCompleted"]}");
                        }
                    }
                }

                if (sb.Length == 0)
                    return "No tasks found.";

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return "Error loading tasks: " + ex.Message;
            }
        }

        // Mark task complete
        public string MarkTaskAsComplete(int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE Tasks SET IsCompleted=1 WHERE Id=@id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                        return "Task marked as completed.";

                    return "Task not found.";
                }
            }
            catch (Exception ex)
            {
                return "Error updating task: " + ex.Message;
            }
        }

        // Delete task
        public string DeleteTask(int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "DELETE FROM Tasks WHERE Id=@id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }

                return "Task deleted.";
            }
            catch (Exception ex)
            {
                return "Error deleting task: " + ex.Message;
            }
        }
    }
}