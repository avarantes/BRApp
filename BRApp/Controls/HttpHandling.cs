using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace BRApp.Controls
{
    public static class HttpHandling
    {
        internal static bool GetAPIHealth()
        {
            bool resultReturn = false;

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://private-anon-ad78d151d5-blissrecruitmentapi.apiary-mock.com/");

                try
                {

                    Task<HttpResponseMessage> response = httpClient.GetAsync("health");
                    response.Wait();

                    HttpResponseMessage result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        Task<string> read = result.Content.ReadAsStringAsync();
                        read.Wait();
                        resultReturn = result.StatusCode == System.Net.HttpStatusCode.OK ? resultReturn = true : resultReturn = false;
                    }
                    else
                    {
                        resultReturn = false;
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.WriteLog(ex);
                }
            }

            return resultReturn;
        }

        internal static Questions RetrieveQuestion(string questionIdToLoad)
        {
            Questions questionReturn=null;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://private-anon-1831d2c376-blissrecruitmentapi.apiary-mock.com/questions/");

                try
                {
                    Task<HttpResponseMessage> response = httpClient.GetAsync(questionIdToLoad);
                    response.Wait();

                    HttpResponseMessage result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        Task<string> read = result.Content.ReadAsStringAsync();
                        read.Wait();
                        questionReturn = JsonConvert.DeserializeObject<Questions>(read.Result);

                    }
                    else
                    {
                        questionReturn = null;
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.WriteLog(ex);
                }
            }

            return questionReturn;

        }

        internal static async Task<bool> VoteQuestionAsync(Questions questionToVote)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://private-anon-1e69ddeb5d-blissrecruitmentapi.apiary-mock.com/");

                try
                {
                    string questionToVoteStringfy = JsonConvert.SerializeObject(questionToVote);
                    using (StringContent content = new StringContent(questionToVoteStringfy, System.Text.Encoding.Default, "application/json"))
                    {
                        using (HttpResponseMessage response = await httpClient.PutAsync($"questions/{questionToVote.Id}", content))
                        {
                            if(response.IsSuccessStatusCode)
                            {
                                return true;
                            }
                            else { 
                                return false;
                            }
                            
                        }
                    }
                }
                catch (Exception ex)
                {                   
                    LogWriter.WriteLog(ex);
                    return false;
                }
            }
        }

        internal static List<Questions> GetAllQuestions()
        {
            List<Questions> questionsDataSource = new List<Questions>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://private-anon-ad78d151d5-blissrecruitmentapi.apiary-mock.com/");

                try
                {
                    Task<HttpResponseMessage> response = httpClient.GetAsync("questions?limit=&offset=&filter=");
                    response.Wait();

                    HttpResponseMessage result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        Task<string> read = result.Content.ReadAsStringAsync();
                        read.Wait();
                        questionsDataSource = JsonConvert.DeserializeObject<List<Questions>>(read.Result);

                    }
                    else
                    {
                        questionsDataSource = null;
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.WriteLog(ex);
                }
            }

            return questionsDataSource;
        }
        
    }


}