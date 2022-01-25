using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace UniversalDataConvertor
{
    class ConvertorSetup
    {
        public ObservableCollection<Mode> Modes { get; set; }

        public ConvertorSetup()
        {
            Modes = new ObservableCollection<Mode>
            {
                new Mode{
                    Name = "Персональные данные",
                    DbFields = new ObservableCollection<DbFieldInfo>
                    {
                        new DbFieldInfo{ Name = "Field 1" },
                        new DbFieldInfo{ Name = "Field 2" },
                        new DbFieldInfo{ Name = "Field 3" }
                    },
                    FileDataSourceName = "Name 1",
                    FileFields = new ObservableCollection<FileFieldInfo>
                    {
                        new FileFieldInfo
                        {
                            Name = "Filed aaa"
                        }
                    }
                },
                new Mode{ 
                    Name = "Назначения",
                    DbFields = new ObservableCollection<DbFieldInfo>
                    {
                        new DbFieldInfo{ Name = "Field Z" },
                        new DbFieldInfo{ Name = "Field Y" },
                        new DbFieldInfo{ Name = "Field X" }
                    }
                },
                new Mode{ Name = "Трудовая деятельность" },
                new Mode{ Name = "Отпуска" },
                new Mode{ Name = "Больничные" },
                new Mode{ Name = "Инвалиды" }
            };
        }
    }
}
