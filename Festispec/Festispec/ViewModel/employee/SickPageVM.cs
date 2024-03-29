﻿using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Employee;
using Festispec.ViewModel.auth;
using Festispec.ViewModel.planning.plannedEmployee;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
using System.Windows.Input;

namespace Festispec.ViewModel.employee
{
    public class SickPageVM : ViewModelBase
    {
        private EmployeeVM _employee;
        private PlannedEmployeeVM _plannedEmployee;
        public string ShowEventInfo { get; set; }
        public string ShowNoEvent { get; set; }
        public string SickPageButton { get; set; }
        public string EventName { get; set; }
        public bool SickButtonDisable { get; set; }
        public string EventStartDate { get; set; }
        public string EventEndDate { get; set; }
        public ICommand SickButtonCommand { get; set; }

        public SickPageVM()
        {
            _employee = UserSessionVM.Current.Employee;
            SickButtonCommand = new RelayCommand(AddSickness);

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if(message.NextPageType == typeof(SickPage))
                {
                    ShowEvent();
                    RaisePropertyChanged(() => SickButtonDisable);
                    RaisePropertyChanged(() => SickPageButton);
                }
            });

            ShowEvent();
        }

        private void ShowEvent()
        {
            using (var context = new Entities())
            {
                var tempPlanning = context.InspectorPlannings.ToList()
                    .Select(e => new PlannedEmployeeVM(e))
                    .Where(e => e.Employee.Id == _employee.Id)
                    .Where(e => e.PlannedStartTime.Date == DateTime.Today)
                    .FirstOrDefault();

                if (tempPlanning != null)
                {
                    _plannedEmployee = tempPlanning;
                    var tempEvent = context.Orders.Include("Event").ToList()
                        .Where(e => e.Id == tempPlanning.OrderId)
                        .FirstOrDefault();

                    if (tempEvent != null)
                    {
                        EventName = tempEvent.Event.Name;
                        EventStartDate = tempEvent.Event.BeginDate.ToString("dd-MM-yyyy HH:mm");
                        EventEndDate = tempEvent.Event.EndDate.ToString("dd-MM-yyyy HH:mm");

                        ShowEventInfo = "Visible";
                        ShowNoEvent = "Hidden";

                        RaisePropertyChanged(() => ShowEventInfo);
                        RaisePropertyChanged(() => ShowNoEvent);
                        RaisePropertyChanged(() => EventName);
                        RaisePropertyChanged(() => EventStartDate);
                        RaisePropertyChanged(() => EventEndDate);

                        if (CheckIfAlreadySick())
                        {
                            SickButtonDisable = false;
                            SickPageButton = "Al ziekgemeld vandaag";
                        }
                        else
                        {
                            SickButtonDisable = true;
                            SickPageButton = "Ziekmelden";
                        }
                    }
                }
                else
                {
                    ShowEventInfo = "Hidden";
                    ShowNoEvent = "Visible";
                }
            }
        }

        private bool CheckIfAlreadySick()
        {
            using (var context = new Entities())
            {
                var tempSickCheck = context.SickReportInspectors.ToList()
                    .Where(e => e.InspectorPlanningEmployeeId == UserSessionVM.Current.Employee.Id)
                    .Where(e => e.InspectorPlanningOrderId == _plannedEmployee.OrderId)
                    .Where(e => e.InspoctorPlanningDayId == _plannedEmployee.DayId)
                    .FirstOrDefault();

                if(tempSickCheck != null)
                {
                    return true;
                }
                return false;
            }
        }

        private void AddSickness()
        {
            using(var context = new Entities())
            {
                var sick = new SickReportInspector();
                sick.EmployeeId = UserSessionVM.Current.Employee.Id;
                sick.InspectorPlanningEmployeeId = UserSessionVM.Current.Employee.Id;
                sick.InspectorPlanningOrderId = _plannedEmployee.OrderId;
                sick.InspoctorPlanningDayId = _plannedEmployee.DayId;
                sick.Reason = "Ziekmelden";

                context.SickReportInspectors.Add(sick);
                context.SaveChanges();

                SickButtonDisable = false;
                SickPageButton = "Al ziekgemeld vandaag";
                RaisePropertyChanged("SickButtonDisable");
                RaisePropertyChanged("SickPageButton");
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowSuccess("Ziekgemeld!");
            }
        }
    }
}
