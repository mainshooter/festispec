using Festispec.Message;
﻿using Festispec.Lib.Enums;
using Festispec.View.Pages.Report.element.Edit;
using System;
using System.Collections.Generic;
using System.Data;

namespace Festispec.ViewModel.report.element
{
    public class TableVM : ReportElementVM
    {
        private DataTable _dataTable;

        public Dictionary<string, List<string>> Dictionary { get; set; }

        public ReportElementVM ReportElementVM { get; set; }


        public DataTable DataTable
        {
            get
            {
                return _dataTable;
            }
            set
            {
                _dataTable = value;
                RaisePropertyChanged("DataTable");
            }
        }

        public TableVM()
        {
            Type = ReportElementType.Table;
        }

        public TableVM(ReportElementVM element)
        {
            EditElement = new GalaSoft.MvvmLight.Command.RelayCommand(() => Edit());
            DataTable = new DataTable();
            Dictionary = new Dictionary<string, List<string>>();
            Id = element.Id;
            Type = element.Type;
            Title = element.Title;
            Content = element.Content;
            Order = element.Order;
            ReportId = element.ReportId;
        }

        public void Edit()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditTablePage) });
            MessengerInstance.Send<ChangeSelectedReportElementMessage>(new ChangeSelectedReportElementMessage()
            {
                ReportElementVM = this
            });
        }

        public void ApplyChanges()
        {
            try
            {
                List<List<string>> dataList = Data;
                var headers = dataList[0];
                foreach (var item in headers)
                {
                    Dictionary.Add(item, new List<string>());
                }
                dataList.RemoveAt(0);
                while (dataList.Count > 0)
                {
                    List<string> dataListItem = dataList[0];
                    int index = 0;
                    foreach (var dicItem in Dictionary)
                    {
                        dicItem.Value.Add(dataListItem[index]);
                        index++;
                    }
                    dataList.RemoveAt(0);
                }
                foreach (var item in Dictionary)
                {
                    DataTable.Columns.Add(item.Key);
                }

                int highestCount = 0;
                foreach (var item in Dictionary)
                {
                    var listCount = item.Value.Count;
                    if (listCount > highestCount)
                    {
                        highestCount = listCount;
                    }
                }

                for (int i = 0; i < highestCount; i++)
                {
                    DataRow dataRow = DataTable.NewRow();
                    int internIndex = 0;
                    foreach (var item in Dictionary)
                    {
                        var list = item.Value;
                        if (list.Count > i)
                        {
                            dataRow[internIndex] = list[i];
                        }
                        else
                        {
                            dataRow[internIndex] = "";
                        }

                        internIndex++;
                    }
                    DataTable.Rows.Add(dataRow);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
