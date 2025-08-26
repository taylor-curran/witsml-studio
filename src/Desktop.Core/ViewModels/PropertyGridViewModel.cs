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
using Caliburn.Micro;
using Energistics.DataAccess;
using PDS.WITSMLstudio.Desktop.Core.Models;
using PDS.WITSMLstudio.Desktop.Core.Runtime;
using WitsmlFramework;

namespace PDS.WITSMLstudio.Desktop.Core.ViewModels
{
    /// <summary>
    /// Manages the behavior of the property grid control.
    /// </summary>
    /// <seealso cref="Caliburn.Micro.Screen" />
    public class PropertyGridViewModel : Screen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyGridViewModel" /> class.
        /// </summary>
        /// <param name="runtime">The runtime service.</param>
        /// <param name="objectData">The object data view model.</param>
        public PropertyGridViewModel(IRuntimeService runtime, WitsmlFramework.ViewModels.DataGridViewModel objectData = null)
        {
            Runtime = runtime;
            ObjectData = objectData;
        }

        /// <summary>
        /// Gets the runtime service.
        /// </summary>
        /// <value>The runtime.</value>
        public IRuntimeService Runtime { get; private set; }

        /// <summary>
        /// Gets the object data view model.
        /// </summary>
        /// <value>The object data view model.</value>
        public WitsmlFramework.ViewModels.DataGridViewModel ObjectData { get; }

        private object _currentObject;

        /// <summary>
        /// Gets or sets the current object.
        /// </summary>
        /// <value>The current object.</value>
        public object CurrentObject
        {
            get { return _currentObject; }
            set
            {
                if (!ReferenceEquals(_currentObject, value))
                {
                    _currentObject = value;
                    NotifyOfPropertyChange(() => CurrentObject);
                }
            }
        }

        /// <summary>
        /// Sets the current object.
        /// </summary>
        /// <param name="objectType">The data object type.</param>
        /// <param name="xml">The XML string.</param>
        /// <param name="version">The WITSML version.</param>
        /// <param name="bindDataGrid">True if grid can be bound with results.</param>
        /// <param name="keepGridData">True if not clearing data when querying partial results.</param>
        /// <param name="retrieveObjectSelection">if set to <c>true</c> the retrieve object selection setting is selected.</param>
        /// <param name="errorHandler">The error handler.</param>
        public void SetCurrentObject(string objectType, string xml, string version, bool bindDataGrid, bool keepGridData, bool retrieveObjectSelection, Action<WitsmlException> errorHandler)
        {
            var document = WitsmlParser.Parse(xml);
            var family = ObjectTypes.GetFamily(objectType);
            var dataType = ObjectTypes.GetObjectGroupType(objectType);
            var dataObject = EnergisticsConverter.XmlToObject<IDataObject>(xml);
            var collection = dataObject as IEnergisticsCollection;

            TypeDecorationManager.Register(typeof(IDataObject));

            CurrentObject = collection == null
                ? dataObject
                : collection.Items.Count > 1
                ? collection.Items
                : collection.Items.Count == 1
                ? collection.Items[0]
                : collection;

                if ((collection == null || collection.Items.Count == 1) && (bindDataGrid || keepGridData))
                {
                        ObjectData?.SetCurrentObject(objectType, CurrentObject, keepGridData, retrieveObjectSelection, errorHandler);
                }
                else
                {
                    ObjectData?.ClearDataTable();
                }
        }
    }
}
