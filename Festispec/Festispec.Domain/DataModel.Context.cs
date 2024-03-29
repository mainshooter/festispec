﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Festispec.Domain
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AvailabilityInspector> AvailabilityInspectors { get; set; }
        public virtual DbSet<BetterReportInspector> BetterReportInspectors { get; set; }
        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<ContactPerson> ContactPersons { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Day> Days { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<ElementType> ElementTypes { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeStatu> EmployeeStatus { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<InspectorPlanning> InspectorPlannings { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<QueryTemplate> QueryTemplates { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<Quotation> Quotations { get; set; }
        public virtual DbSet<QuotationStatu> QuotationStatus { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<ReportElement> ReportElements { get; set; }
        public virtual DbSet<ReportStatu> ReportStatus { get; set; }
        public virtual DbSet<SickReportInspector> SickReportInspectors { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<SurveyStatu> SurveyStatus { get; set; }
    }
}
