﻿using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Planning;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.planning.plannedEmployee;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Data.Entity;
using System.Windows.Input;

namespace Festispec.ViewModel.planning
{
    public class EditPlannedEmployeeVM : ViewModelBase
    {
        private PlannedEmployeeVM _plannedEmployeeVM;
        private EventVM _eventVM;
        private DateTime _plannedStartTime;
        private DateTime _plannedEndTime;

        public ICommand BackCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
               
        public EventVM EventVM
        {
            get
            {
                return _eventVM;
            }
            set
            {
                _eventVM = value;
                RaisePropertyChanged(() => EventVM);
            }
        }

        public PlannedEmployeeVM PlannedEmployeeVM
        {
            get
            {
                return _plannedEmployeeVM;
            }
            set
            {
                _plannedEmployeeVM = value;
                RaisePropertyChanged(() => PlannedEmployeeVM);
            }
        }

        public EditPlannedEmployeeVM()
        {
            this.MessengerInstance.Register<ChangeSelectedPlannedEmployeeMessage>(this, message =>
            {
                PlannedEmployeeVM = message.PlannedEmployee;
                EventVM = message.EventVM;
                _plannedStartTime = PlannedEmployeeVM.PlannedStartTime;
                _plannedEndTime = PlannedEmployeeVM.PlannedEndTime;
            });
            BackCommand = new RelayCommand(Back);
            SaveChangesCommand = new RelayCommand(EditPlannedEmployee, CanSave);
        }

        private bool CanSave()
        {
            if (PlannedEmployeeVM == null || PlannedEmployeeVM.PlannedStartTime >= PlannedEmployeeVM.PlannedEndTime)
            {
                return false;
            }
            return true;
        }

        private void EditPlannedEmployee()
        {
            PlannedEmployeeVM.WorkStartTime = PlannedEmployeeVM.PlannedStartTime;
            PlannedEmployeeVM.WorkEndTime = PlannedEmployeeVM.PlannedEndTime;
            using (var context = new Entities())
            {
                context.InspectorPlannings.Attach(PlannedEmployeeVM.ToModel());
                context.Entry(PlannedEmployeeVM.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(PlanningOverviewPage) });
        }

        private void Back()
        {
            PlannedEmployeeVM.PlannedStartTime = _plannedStartTime;
            PlannedEmployeeVM.PlannedEndTime = _plannedEndTime;
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(PlanningOverviewPage) });
        }
    }
}
