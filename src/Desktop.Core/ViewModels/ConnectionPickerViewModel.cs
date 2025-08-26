﻿//----------------------------------------------------------------------- 
// PDS WITSMLstudio Desktop, 2018.1
//
// Copyright 2018 PDS Americas LLC
// 
// Licensed under the PDS Open Source WITSML Product License Agreement (the
// "License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//     http://www.pds.group/WITSMLstudio/OpenSource/ProductLicenseAgreement
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Caliburn.Micro;
using Newtonsoft.Json;
using PDS.WITSMLstudio.Connections;
using PDS.WITSMLstudio.Framework;
using PDS.WITSMLstudio.Desktop.Core.Properties;
using PDS.WITSMLstudio.Desktop.Core.Runtime;
using WitsmlFramework;
using WitsmlFramework.Extensions;

namespace PDS.WITSMLstudio.Desktop.Core.ViewModels
{
    /// <summary>
    /// Manages the behavior of the connection drop down list control.
    /// </summary>
    /// <seealso cref="Caliburn.Micro.Screen" />
    public class ConnectionPickerViewModel : Screen
    {
        private static readonly Connection _selectConnectionItem = new Connection { Name = "Select Connection..." };
        private static readonly Connection _addNewConnectionItem = new Connection { Name = "(Add New Connection...)" };
        private static readonly string _connectionListBaseFileName = Settings.Default.ConnectionListBaseFileName;
        private static readonly object _connectionsLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionPickerViewModel" /> class.
        /// </summary>
        /// <param name="runtime">The runtime.</param>
        /// <param name="connectionType">The connection type.</param>
        public ConnectionPickerViewModel(IRuntimeService runtime, ConnectionType connectionType)
        {
            Runtime = runtime;
            ConnectionType = connectionType;
            Connections = new BindableCollection<Connection>();
        }

        /// <summary>
        /// Gets the runtime service.
        /// </summary>
        /// <value>The runtime service.</value>
        public IRuntimeService Runtime { get; }

        /// <summary>
        /// Gets the type of the connection.
        /// </summary>
        /// <value>The type of the connection.</value>
        public ConnectionType ConnectionType { get; }

        /// <summary>
        /// Gets or sets a value indicating whether auto connect is enabled.
        /// </summary>
        /// <value><c>true</c> if auto connect is enabled; otherwise, <c>false</c>.</value>
        public bool AutoConnectEnabled { get; set; }

        /// <summary>
        /// Gets or sets the delegate that will be invoked when the selected connection changes.
        /// </summary>
        /// <value>The delegate that will be invoked.</value>
        public Func<Connection, Task> OnConnectionChanged { get; set; }

        private BindableCollection<Connection> _connections;
        /// <summary>
        /// Gets the collection of connections.
        /// </summary>
        /// <value>The collection of connections.</value>
        public BindableCollection<Connection> Connections
        {
            get
            {
                return _connections;
            }
            set
            {
                _connections = value;
                BindingOperations.EnableCollectionSynchronization(_connections, _connectionsLock);
            }
        }

        private Connection _connection;

        /// <summary>
        /// Gets or sets the selected connection.
        /// </summary>
        /// <value>The selected connection.</value>
        public Connection Connection
        {
            get { return _connection; }
            set
            {
                if (!ReferenceEquals(_connection, value))
                {
                    var previous = _connection;
                    _connection = value;
                    NotifyOfPropertyChange(() => Connection);
                    Runtime.Invoke(() => OnSelectedConnectionChanged(previous));
                }
            }
        }

        /// <summary>
        /// Sets the specified connection in the drop down list.
        /// </summary>
        /// <param name="connectionNameOrUri">The connection name or URI.</param>
        /// <returns></returns>
        public bool SelectConnection(string connectionNameOrUri)
        {
            var connection = 
                Connections.FirstOrDefault(c => c.Name.EqualsIgnoreCase(connectionNameOrUri)) ??
                Connections.FirstOrDefault(c => c.Uri.EqualsIgnoreCase(connectionNameOrUri));

            if (connection == null)
                return false;

            Connection = connection;
            return true;
        }

        /// <summary>
        /// Shows the connection dialog to add or update connection settings.
        /// </summary>
        public void ShowConnectionDialog(Connection connection = null)
        {
            var existing = Connections
                .Where(x => x != connection)
                .Select(x => x.Name)
                .ToArray();

            var viewModel = new ConnectionViewModel(Runtime, ConnectionType)
            {
                DataItem = connection ?? new Connection(),
                ConnectionNames = existing
            };

            Runtime.Invoke(() =>
            {
                if (Runtime.ShowDialog(viewModel))
                {
                    // Ensure connection has a Name specified
                    if (string.IsNullOrWhiteSpace(viewModel.DataItem.Name))
                        viewModel.DataItem.Name = viewModel.DataItem.Uri;

                    // Initialize collection of new connection items
                    var connections = (connection == null)
                        ? new[] { viewModel.DataItem }
                        : new Connection[0];

                    // Reset Connections list
                    connection = connection ?? viewModel.DataItem;
                    InsertConnections(connections, connection);
                }
            });
        }

        /// <summary>
        /// Edits the connection using the Connection dialog.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        public void EditConnection(Connection connection, MouseButtonEventArgs e)
        {
            e.Handled = true;
            ShowConnectionDialog(connection);
        }

        /// <summary>
        /// Deletes the connection using the Connection dialog.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        public void DeleteConnection(Connection connection, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (Runtime.ShowConfirm($"Are you sure you want to delete the connection?\nName: { connection.Name }"))
            {
                var selected = connection == Connection
                    ? _selectConnectionItem
                    : Connection;

                Connections.Remove(connection);
                InsertConnections(new Connection[0], selected, true);
            }
        }

        /// <summary>
        /// Automatically connects using the previously used connection settings.
        /// </summary>
        public void AutoConnect()
        {
            if (Connection != _selectConnectionItem) return;
            Connection = GetAutoConnection();
        }

        /// <summary>
        /// Initializes the connections.
        /// </summary>
        public void InitializeConnections()
        {
            if (Connections.Any()) return;

            var connections = LoadConnectionsFromFile();
            var connection = GetAutoConnection(connections);

            InsertConnections(connections, connection);
        }

        /// <summary>
        /// Called when an attached view's Loaded event fires.
        /// </summary>
        /// <param name="view"></param>
        protected override void OnViewLoaded(object view)
        {
            InitializeConnections();
        }

        private void OnSelectedConnectionChanged(Connection previous)
        {
            if (Connection == null) return;
            if (Connection == _selectConnectionItem) return;
            if (Connection == _addNewConnectionItem)
            {
                Task.Delay(100)
                    .ContinueWith(task =>
                    {
                        _connection = previous;
                        NotifyOfPropertyChange(() => Connection);
                    })
                    .ContinueWith(task =>
                    {
                        ShowConnectionDialog();
                    });
            }
            else
            {
                // Save selected connection to file to enable auto-connect
                var viewModel = new ConnectionViewModel(Runtime, ConnectionType);
                viewModel.SaveConnectionFile(Connection);

                // Invoke delegate that will handle the connection change
                OnConnectionChanged?.Invoke(Connection);
            }
        }

        private void InsertConnections(Connection[] connections, Connection selected, bool force = false)
        {
            var names = connections.Select(x => x.Name).ToArray();

            var list = Connections
                .Skip(1)
                .Take(Connections.Count - 2)
                .Where(x => !names.Contains(x.Name))
                .Concat(connections)
                .OrderBy(x => x.Name)
                .ToList();

            if (Connections.Any() || force)
            {
                SaveConnectionsToFile(list);
            }

            Connections.Clear();
            Connections.Add(_selectConnectionItem);
            Connections.AddRange(list);
            Connections.Add(_addNewConnectionItem);
            Connection = selected;
        }

        private Connection[] LoadConnectionsFromFile()
        {
            var fileName = GetConnectionFileName();

            if (File.Exists(fileName))
            {
                //_log.DebugFormat("Reading persisted Connection from '{0}'", filename);
                var json = File.ReadAllText(fileName);
                var connections = JsonConvert.DeserializeObject<List<Connection>>(json);

                connections.ForEach(x =>
                {
                    x.Password = x.Password.Decrypt();
                    x.SecurePassword = x.Password.ToSecureString();
                    x.ProxyPassword = x.ProxyPassword.Decrypt();
                    x.SecureProxyPassword = x.ProxyPassword.ToSecureString();
                });

                return connections.ToArray();
            }

            return new Connection[0];
        } 

        private void SaveConnectionsToFile(List<Connection> connections)
        {
            Runtime.EnsureDataFolder();
            var fileName = GetConnectionFileName();
            //_log.DebugFormat("Persisting Connection to '{0}'", filename);

            connections.ForEach(x =>
            {
                x.Password = x.Password.Encrypt();
                x.ProxyPassword = x.ProxyPassword.Encrypt();
            });

            File.WriteAllText(fileName, JsonConvert.SerializeObject(connections));

            connections.ForEach(x =>
            {
                x.Password = x.Password.Decrypt();
                x.ProxyPassword = x.ProxyPassword.Decrypt();
            });
        }

        private Connection GetAutoConnection(IEnumerable<Connection> connections = null)
        {
            if (!AutoConnectEnabled)
                return _selectConnectionItem;

            var viewModel = new ConnectionViewModel(Runtime, ConnectionType);
            var connection = viewModel.OpenConnectionFile();

            connections = connections ?? Connections;

            // Auto-connect to previous connection, if possible
            return connections.FirstOrDefault(x => x.Name == connection?.Name)
                   ?? _selectConnectionItem;
        }

        /// <summary>
        /// Gets the connection list file name.
        /// </summary>
        /// <returns>The path and file name for the connection list file with format "[data-folder]/[connection-type]ConnectionList.json".</returns>
        internal string GetConnectionFileName()
        {
            return $"{Runtime.DataFolderPath}\\{ConnectionType}{_connectionListBaseFileName}";
        }
    }
}
