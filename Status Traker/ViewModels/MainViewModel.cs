using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using static Status_Traker.Models.JobModel;

namespace Status_Traker.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private VehicleRepairJob? _selectedJob;
        private RepairStatus? _statusFilter;
        
        public ObservableCollection<VehicleRepairJob> Jobs { get; } =
            new ObservableCollection<VehicleRepairJob>();

        public VehicleRepairJob? SelectedJob
        {
            get => _selectedJob;
            set
            {
                _selectedJob = value;
                OnPropertyChanged();
            }
        }

        public RepairStatus? StatusFilter
        {
            get => _statusFilter;
            set
            {
                _statusFilter = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        public RepairStatus? StatusUpdate
        {
            get => _statusFilter;
            set
            {
                _statusFilter = value;
                OnPropertyChanged();                
            }
        }

        public ObservableCollection<RepairStatus> AvailableStatuses { get; } =
            new ObservableCollection<RepairStatus>(
                Enum.GetValues(typeof(RepairStatus)).Cast<RepairStatus>());

        public ICommand SetStatusCommand { get; }

        private readonly ObservableCollection<VehicleRepairJob> _Jobs =
            new ObservableCollection<VehicleRepairJob>();

        public MainViewModel()
        {
           
            SetStatusCommand = new RelayCommand(statusObj =>
            {
                if (statusObj is RepairStatus status)
                    SetStatus(status);
            }, _ => SelectedJob != null);

            SeedSampleData();
        }

        private void SeedSampleData()
        {
            _Jobs.Add(new VehicleRepairJob
            {
                Id = 1,
                CustomerName = "James Howlett",
                Year = "2018",
                Make = "Toyota",
                Model = "Camry SE",
                RepairCenter = "Thunder Bay",               
                Status = RepairStatus.InProgress,
                
            });

            _Jobs.Add(new VehicleRepairJob
            {
                Id = 2,
                CustomerName = "Kurt Wagner",
                Year = "2019",
                Make = "Subaru",
                Model = "Outback",
                RepairCenter = "Chicago",                
                Status = RepairStatus.Received,
            });

            _Jobs.Add(new VehicleRepairJob
            {
                Id = 2,
                CustomerName = "Remy LeBeau",
                Year = "2019",
                Make = "Toyota",
                Model = "Prius",
                RepairCenter = "New Orleans",
                Status = RepairStatus.WaitingOnParts,
            });

            _Jobs.Add(new VehicleRepairJob
            {
                Id = 2,
                CustomerName = "Anna Marie",
                Year = "2014",
                Make = "Honda",
                Model = "Accord",
                RepairCenter = "Dallas",
                Status = RepairStatus.QualityCheck,
            });

            _Jobs.Add(new VehicleRepairJob
            {
                Id = 2,
                CustomerName = "Betsy Braddock",
                Year = "2019",
                Make = "Mazda",
                Model = "MX-5 Miata RF",
                RepairCenter = "Tokyo",                
                Status = RepairStatus.Completed,
            });

            ApplyFilter();
        }

        private void ApplyFilter()
        {
            Jobs.Clear();
            var query = _Jobs.AsEnumerable();
            if (StatusFilter.HasValue)
                    query = query.Where(j => j.Status == StatusFilter.Value);

            foreach (var job in query)
                Jobs.Add(job);
        }

        private void SetStatus(RepairStatus status)
        {
            if (SelectedJob == null) return;
            SelectedJob.Status = status;
            OnPropertyChanged(nameof(SelectedJob));
            Jobs.Clear();
            var query = _Jobs.AsEnumerable();
            foreach (var job in query)
                Jobs.Add(job);
            //ApplyFilter();
        }
    }
}
