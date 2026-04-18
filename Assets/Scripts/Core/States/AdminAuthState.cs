using RUS95.SpinWinEventApp.Core;
using RUS95.SpinWinEventApp.Data;
using RUS95.SpinWinEventApp.Systems.Persistence;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RUS95.SpinWinEventApp.Systems
{
    public class AdminAuthState : IGameState
    {
        private readonly GameFlowController _controller;
        private readonly PlayFabDataFetcher _fetcher;
        private const string AdminPassword = "111222";

        public AdminAuthState(GameFlowController controller)
        {
            _controller = controller;
            _fetcher = new PlayFabDataFetcher();
        }

        public void Enter()
        {
            var ui = _controller.GetUIManager();

            ui.ShowAdminScreen();

            ui.OnAdminSubmit += HandleSubmit;
            ui.OnAdminCancel += HandleCancel;

            ui.OnAdminDownloadClicked += HandleDownload;
        }

        public void Exit()
        {
            var ui = _controller.GetUIManager();

            ui.OnAdminSubmit -= HandleSubmit;
            ui.OnAdminCancel -= HandleCancel;

            ui.OnAdminDownloadClicked -= HandleDownload;
        }

        private void HandleSubmit(string password)
        {
            if (password == AdminPassword)
            {
                _controller.GetUIManager().ShowAdminPanel();
            }
            else
            {
                _controller.GetUIManager().ShowError("Incorrect password");
            }
        }

        private void HandleCancel()
        {
            _controller.SetState(new IdleState(_controller));
        }

        private void HandleDownload()
        {
            DownloadCsv();
        }

        private void DownloadCsv()
        {
            var ui = _controller.GetUIManager();

            try
            {
                string path = System.IO.Path.Combine(
                    UnityEngine.Application.persistentDataPath,
                    "SpinWinData.csv"
                );

                if (!System.IO.File.Exists(path))
                {
                    ui.ShowError("No data available");
                    return;
                }

                string csv = System.IO.File.ReadAllText(path);

                new WebGLFileSaver().Save(csv);

                ui.ShowSuccess("Downloaded successfully");
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError(ex);
                ui.ShowError("Download failed");
            }
        }

        private string ConvertToCsv(List<GameSessionData> dataList)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("DateTime,Result,Name,Email,Phone");

            foreach (var data in dataList)
            {
                string result = data.IsWin ? "WIN" : "LOSE";

                sb.AppendLine(
                    $"{System.DateTime.Now:yyyy-MM-dd HH:mm:ss}," +
                    $"{result}," +
                    $"{Escape(data.Name)}," +
                    $"{Escape(data.Email)}," +
                    $"{Escape(data.Phone)}"
                );
            }

            return sb.ToString();
        }

        private string Escape(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";

            if (value.Contains(","))
                return $"\"{value}\"";

            return value;
        }
    }
}