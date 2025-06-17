using MQTTnet.Client;
using MQTTnet;
using System.Text.RegularExpressions;
using employee_front_end.Model;
using employee_front_end.Event;
using System.Text.Json;

namespace employee_front_end.Infrastructure
{
    public class UnsService : IDisposable
    {
        public List<ApplicationUser> _listApplicationUsers = new List<ApplicationUser>();
        public List<UsersEachLocation> _listLocations = new List<UsersEachLocation>();
        private IMqttClient? _mqttClientUsers;
        private IMqttClient? _mqttClientLocations;
        private readonly UnsEvents _unsEvents;

        public UnsService(UnsEvents unsEvents)
        {
            _unsEvents = unsEvents;
        }

        public void Dispose()
        {
            _mqttClientUsers?.Dispose();
            _mqttClientUsers = null;
            _mqttClientLocations?.Dispose();
            _mqttClientLocations = null;
        }


        public async Task<List<ApplicationUser>> SubscribeUsersAsync(CancellationToken cancellationToken)
        {
            if (_mqttClientUsers is null)
            {
                var mqttFactory = new MqttFactory();
                _mqttClientUsers = mqttFactory.CreateMqttClient();
                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("emqx").Build();
                await _mqttClientUsers.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(
                        f =>
                        {
                            f.WithTopic("users/#");
                        })
                    .Build();

                _mqttClientUsers.ApplicationMessageReceivedAsync += async message =>
                {
                    string messagePayLoad = System.Text.Encoding.UTF8.GetString(message.ApplicationMessage.Payload);
                    string messageTopic = message.ApplicationMessage.Topic;

                    ApplicationUser applicationUser = new ApplicationUser();
                    applicationUser.PersonalInfo = new PersonalInfo();

                    Dictionary<string, bool> dicRegexTopic = new Dictionary<string, bool>();
                    string id = "";

                    (dicRegexTopic,id) = await CreateDictionaryRegexAsync(messageTopic,cancellationToken);
                    applicationUser.Id = id;

                    var userInList = _listApplicationUsers.Find(x => x.Id == id);

                    if (userInList != null)
                    {
                        applicationUser.ScheduleWorkNow = userInList.ScheduleWorkNow;
                        applicationUser.TodayShift = userInList.TodayShift;
                        applicationUser.TodayAbsent = userInList.TodayAbsent;
                        applicationUser.Checked_In = userInList.Checked_In;
                        applicationUser.PersonalInfo = userInList.PersonalInfo;
                        applicationUser.RoleId = userInList.RoleId;
                        _listApplicationUsers.RemoveAll(x => x.Id == id);
                    }
                    applicationUser = await CreateUserToAddAsync(dicRegexTopic,applicationUser,messagePayLoad, cancellationToken);
                    _listApplicationUsers.Add(applicationUser);
                    _unsEvents.CallBackApplicationUsers();
                };
                await _mqttClientUsers.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
            }
            _listApplicationUsers = _listApplicationUsers.OrderBy(x => x.PersonalInfo.Name).ToList();
            return _listApplicationUsers;
        }
        public async Task<(Dictionary<string, bool>, string)> CreateDictionaryRegexAsync(string messageTopic , CancellationToken cancellationToken)
        {
            Dictionary<string, bool> dicRegexTopic = new Dictionary<string, bool>();

            string patternId = @"(?<=users/)\d+(?=/)";
            string patternSchedule = @"\b(schedule)\b$";
            string patternToday = @"\b(shift_today)\b$";
            string patternCheckedIn = @"\b(checked_in)\b$";
            string patternAbsentToday = @"\b(absent_today)\b$";

            string patternName = @"\b(name)\b$";
            string patternEmail = @"\b(email)\b$";
            string patternSAMAcountNumber = @"\b(sam_account_name)\b";
            string patternEmployeeNumber = @"\b(employee_number)\b$";



            Match matchId = Regex.Match(messageTopic, patternId);
            bool hasMatchWorkNow = Regex.IsMatch(messageTopic, patternSchedule);
            bool hasMatchTodayShift = Regex.IsMatch(messageTopic, patternToday);
            bool hasMatchCheckedIn = Regex.IsMatch(messageTopic, patternCheckedIn);
            bool hasMatchAbsentToday = Regex.IsMatch(messageTopic, patternAbsentToday);

            bool hasMatchName = Regex.IsMatch(messageTopic, patternName);
            bool hasMatchEmail = Regex.IsMatch(messageTopic, patternEmail);
            bool hasMatchSAMAcountNumber = Regex.IsMatch(messageTopic, patternSAMAcountNumber);
            bool hasMatchEmployeeNumber = Regex.IsMatch(messageTopic, patternEmployeeNumber);

            dicRegexTopic.Add("MatchTopicWorkNow", hasMatchWorkNow);
            dicRegexTopic.Add("MatchTopicTodayShift", hasMatchTodayShift);
            dicRegexTopic.Add("MatchTopicCheckedIn", hasMatchCheckedIn);
            dicRegexTopic.Add("MatchTopicAbsentToday", hasMatchAbsentToday);

            dicRegexTopic.Add("MatchTopicName", hasMatchName);
            dicRegexTopic.Add("MatchTopicEmail", hasMatchEmail);
            dicRegexTopic.Add("MatchTopicSAMAcountNumber", hasMatchSAMAcountNumber);
            dicRegexTopic.Add("MatchTopicEmployeeNumber", hasMatchEmployeeNumber);

            string id = Convert.ToString(matchId.Value);

            return (dicRegexTopic,id);

        }

        public async Task<ApplicationUser> CreateUserToAddAsync(Dictionary<string, bool> dicRegexTopic,
            ApplicationUser applicationUser,string messagePayLoad,CancellationToken cancellationToken)
        {
            string patternTrueFalse = @"(true)|(false)";

            if (dicRegexTopic["MatchTopicName"] is true)
            {
                string name = Convert.ToString(messagePayLoad);
                name = name.Replace("\"", "");
                applicationUser.PersonalInfo.Name = name;
            }
            if (dicRegexTopic["MatchTopicWorkNow"] is true)
            {
                var matchTrueFalse = Regex.Match(messagePayLoad, patternTrueFalse);
                string scheduleWorkNowString = Convert.ToString(matchTrueFalse);
                bool scheduleWorkNow = Convert.ToBoolean(scheduleWorkNowString);
                applicationUser.ScheduleWorkNow = scheduleWorkNow;
            }
            if (dicRegexTopic["MatchTopicTodayShift"] is true)
            {
                var listFrom = new List<string>();
                var listReason = new List<string>();
                var listTill = new List<string>();

                List<TodayShift> listShifts = JsonSerializer.Deserialize<List<TodayShift>>(messagePayLoad);

                foreach (var shift in listShifts)
                {
                    listFrom.Add(shift.From);
                    listTill.Add(shift.Till);

                }

                if (listFrom.Any() & listTill.Any())
                {

                    var fromString = String.Join(System.Environment.NewLine, listFrom);
                    var tillString = String.Join(System.Environment.NewLine, listTill);

                    var todayShift = new TodayShift
                    {
                        From = fromString,
                        Till = tillString,
                    };

                    applicationUser.TodayShift = todayShift;
                }
                else
                {
                    var todayShift = new TodayShift();
                    applicationUser.TodayShift = todayShift;
                }

            }
            if (dicRegexTopic["MatchTopicAbsentToday"] is true)
            {
                var listFrom = new List<string>();
                var listReason = new List<string>();
                var listTill = new List<string>();

                List<TodayAbsent> listAbsents = JsonSerializer.Deserialize<List<TodayAbsent>>(messagePayLoad);

                foreach (var absent in listAbsents)
                {
                    listFrom.Add(absent.From);
                    listTill.Add(absent.Till);
                    listReason.Add(absent.Reason);

                }

                if (listFrom.Any() & listTill.Any())
                {

                    var fromString = String.Join(System.Environment.NewLine, listFrom);
                    var tillString = String.Join(System.Environment.NewLine, listTill);
                    var reasonString = String.Join(System.Environment.NewLine, listReason);
                    
                    var todayAbsent = new TodayAbsent
                    {
                        From = fromString,
                        Till = tillString,
                        Reason = reasonString
                    };
                    applicationUser.TodayAbsent = todayAbsent;
                }
                else
                {
                    applicationUser.TodayAbsent = new TodayAbsent();
                }
            }
            if (dicRegexTopic["MatchTopicCheckedIn"] is true)
            {
                Match matchTrueFalse = Regex.Match(messagePayLoad, patternTrueFalse);
                string checkedInString = Convert.ToString(matchTrueFalse);
                bool checkedIn = Convert.ToBoolean(checkedInString);
                applicationUser.Checked_In = checkedIn;
            }
            if (dicRegexTopic["MatchTopicEmail"] is true)
            {
                string email = Convert.ToString(messagePayLoad);
                email = email.Replace("\"", "");
                applicationUser.PersonalInfo.Email = email;
            }
            if (dicRegexTopic["MatchTopicSAMAcountNumber"] is true)
            {
                string sAMAcountName = Convert.ToString(messagePayLoad);
                sAMAcountName = sAMAcountName.Replace("\"", "");
                applicationUser.PersonalInfo.SAMAcountName = sAMAcountName;
            }
            if (dicRegexTopic["MatchTopicEmployeeNumber"] is true)
            {
                string employeeNumber = Convert.ToString(messagePayLoad);
                employeeNumber = employeeNumber.Replace("\"", "");
                applicationUser.PersonalInfo.EmployeeNumber = employeeNumber;
            }
            return applicationUser;
        }


        public async Task<List<UsersEachLocation>> SubscribeLocationsAsync(CancellationToken cancellationToken)
        {
            if (_mqttClientLocations is null)
            {
                var mqttFactory = new MqttFactory();
                _mqttClientLocations = mqttFactory.CreateMqttClient();
                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("emqx").Build();
                await _mqttClientLocations.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(
                        f =>
                        {
                            f.WithTopic($"locations/#");
                        })
                    .Build();

                _mqttClientLocations.ApplicationMessageReceivedAsync += async message =>
                {
                    string messagePayLoad = System.Text.Encoding.UTF8.GetString(message.ApplicationMessage.Payload);
                    string messageTopic = message.ApplicationMessage.Topic;

                    string patternLocation = @"(?<=locations\/)(.*)(?=\/pvs)";
                    var matchLocation = Convert.ToString(Regex.Match(messageTopic, patternLocation));
                    List<string> listStringId = new List<string> { messagePayLoad };

                    var location = _listLocations.FirstOrDefault(x => x.Location == matchLocation);
                    var newUsersEachLocation = new UsersEachLocation();
                    newUsersEachLocation.GuidList = listStringId;
                    if (location is not null)
                    {
                        newUsersEachLocation.Location = location.Location;
                    }
                    else
                    {
                        newUsersEachLocation.Location = matchLocation;
                    }

                    if (location is not null)
                    {
                        _listLocations.Remove(location);
                        _listLocations.Add(newUsersEachLocation);
                    }
                    else
                    {
                        _listLocations.Add(newUsersEachLocation);
                    }
                };

                await _mqttClientLocations.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);           
            }

            return _listLocations;

        }


        public async Task<List<UsersEachLocation>> GetLocationsAsync(CancellationToken cancellationToken)
        {
            _listLocations = _listLocations.OrderBy(x => x.Location).ToList();
            return _listLocations;
        }
        public async Task<List<ApplicationUser>> GetApplicationUsersAsync(CancellationToken cancellationToken)
        {
            _listApplicationUsers = _listApplicationUsers.OrderBy(x => x.PersonalInfo.Name).ToList();
            return _listApplicationUsers;
        }

    }

}
