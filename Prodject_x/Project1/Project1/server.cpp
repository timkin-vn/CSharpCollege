// server_telnet.cpp
#include <iostream>
#include <string>
#include <vector>
#include <map>
#include <chrono>
#include <winsock2.h>
#include <ws2tcpip.h>
#pragma comment(lib, "ws2_32.lib")

using namespace std;

enum Choice { ROCK, PAPER, SCISSORS, LIZARD, SPOCK, QUIT, DRAW };
enum GameMode { HUMAN_HUMAN, HUMAN_COMPUTER, COMPUTER_COMPUTER };

struct GameStats {
    map<Choice, int> choiceFrequency;
    vector<pair<Choice, Choice>> roundHistory;
    vector<int> gameResults;
    chrono::duration<double> gameDuration;
};

class Game {
private:
    int roundsLeft = 5;
    int player1Score = 0;
    int player2Score = 0;
    int gamesPlayed = 0;
    const int TOTAL_GAMES = 3;
    GameMode mode;
    GameStats stats;

    int determineWinner(Choice p1, Choice p2) {
        if (p1 == p2) return 0;
        if (p1 == QUIT) return -1;
        if (p2 == QUIT) return 1;
        if (p1 == DRAW && p2 == DRAW) return 0;

        if ((p1 == ROCK && (p2 == SCISSORS || p2 == LIZARD)) ||
            (p1 == PAPER && (p2 == ROCK || p2 == SPOCK)) ||
            (p1 == SCISSORS && (p2 == PAPER || p2 == LIZARD)) ||
            (p1 == LIZARD && (p2 == SPOCK || p2 == PAPER)) ||
            (p1 == SPOCK && (p2 == SCISSORS || p2 == ROCK)))
            return 1;
        return -2;
    }

    Choice getComputerChoice() {
        return static_cast<Choice>(rand() % 5);
    }

public:
    Game(GameMode m) : mode(m) {}

    string choiceToString(Choice c) {
        switch (c) {
        case ROCK: return "Rock";
        case PAPER: return "Paper";
        case SCISSORS: return "Scissors";
        case LIZARD: return "Lizard";
        case SPOCK: return "Spock";
        case DRAW: return "Draw";
        case QUIT: return "Quit";
        default: return "Unknown";
        }
    }

    pair<int, string> playRound(Choice p1Choice, Choice p2Choice = ROCK) {
        string result;
        if (mode == HUMAN_COMPUTER) p2Choice = getComputerChoice();
        else if (mode == COMPUTER_COMPUTER) {
            p1Choice = getComputerChoice();
            p2Choice = getComputerChoice();
        }

        stats.choiceFrequency[p1Choice]++;
        stats.choiceFrequency[p2Choice]++;
        stats.roundHistory.push_back({ p1Choice, p2Choice });

        int roundWinner = determineWinner(p1Choice, p2Choice);
        roundsLeft--;

        result = "P1 chose " + choiceToString(p1Choice) + ", P2 chose " + choiceToString(p2Choice) + ". ";
        if (roundWinner == 0) result += "Draw!\n";
        else if (roundWinner == 1) {
            player1Score++;
            result += "Player 1 wins round!\n";
        }
        else if (roundWinner == -1) {
            result += "Player 1 surrendered!\n";
            roundsLeft = 0;
        }
        else if (roundWinner == -2) {
            player2Score++;
            result += "Player 2 wins round!\n";
        }
        result += "Score: P1 " + to_string(player1Score) + " - P2 " + to_string(player2Score) + "\n";
        return { roundWinner, result };
    }

    string showStats() {
        string statsStr = "\nGame Statistics:\n";
        for (const auto& round : stats.roundHistory) {
            statsStr += "P1: " + choiceToString(round.first) + " vs P2: " + choiceToString(round.second) + "\n";
        }

        Choice mostPopular = ROCK, leastPopular = ROCK;
        int maxFreq = 0, minFreq = INT_MAX;
        for (const auto& freq : stats.choiceFrequency) {
            if (freq.second > maxFreq) {
                maxFreq = freq.second;
                mostPopular = freq.first;
            }
            if (freq.second < minFreq && freq.second > 0) {
                minFreq = freq.second;
                leastPopular = freq.first;
            }
        }
        statsStr += "Most popular choice: " + choiceToString(mostPopular) + "\n";
        statsStr += "Least popular choice: " + choiceToString(leastPopular) + "\n";
        return statsStr;
    }

    bool isGameOver() { return roundsLeft <= 0; }
    bool isMatchOver() { return gamesPlayed >= TOTAL_GAMES; }
    void newGame() {
        roundsLeft = 5;
        gamesPlayed++;
        stats.gameResults.push_back(player1Score > player2Score ? 1 :
            player2Score > player1Score ? 2 : 0);
        player1Score = player2Score = 0;
    }
};

int main() {
    WSADATA wsaData;
    WSAStartup(MAKEWORD(2, 2), &wsaData);

    SOCKET serverSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    sockaddr_in serverAddr;
    serverAddr.sin_family = AF_INET;
    serverAddr.sin_addr.s_addr = INADDR_ANY;
    serverAddr.sin_port = htons(8888);

    bind(serverSocket, (sockaddr*)&serverAddr, sizeof(serverAddr));
    listen(serverSocket, 1);

    cout << "Server waiting for connection on port 8888...\n";
    SOCKET clientSocket = accept(serverSocket, NULL, NULL);
    cout << "Client connected!\n";

    // Приветственное сообщение
    string welcome = "Welcome to Rock-Paper-Scissors-Lizard-Spock!\n"
        "Select mode (send number):\n"
        "1 - Human vs Computer\n"
        "2 - Computer vs Computer\n"
        "Choices: 0-Rock, 1-Paper, 2-Scissors, 3-Lizard, 4-Spock, 5-Quit, 6-Draw\n";
    send(clientSocket, welcome.c_str(), welcome.length(), 0);

    // Получение режима игры
    char buffer[1024];
    int bytesReceived = recv(clientSocket, buffer, sizeof(buffer), 0);
    buffer[bytesReceived] = '\0';
    int modeChoice = atoi(buffer) - 1;
    if (modeChoice < 0 || modeChoice > 1) modeChoice = 0;
    Game game(static_cast<GameMode>(modeChoice + 1));

    string modeMsg = "Mode selected: " + string(modeChoice == 0 ? "Human vs Computer" : "Computer vs Computer") + "\n";
    send(clientSocket, modeMsg.c_str(), modeMsg.length(), 0);

    while (!game.isMatchOver()) {
        if (modeChoice == 0) { // Human vs Computer
            bytesReceived = recv(clientSocket, buffer, sizeof(buffer), 0);
            if (bytesReceived <= 0) break;
            buffer[bytesReceived] = '\0';
            int choiceNum = atoi(buffer);
            Choice p1Choice = static_cast<Choice>(choiceNum);

            pair<int, string> roundResult = game.playRound(p1Choice);
            int roundWinner = roundResult.first;
            string result = roundResult.second;
            send(clientSocket, result.c_str(), result.length(), 0);

            if (game.isGameOver()) {
                string stats = game.showStats();
                send(clientSocket, stats.c_str(), stats.length(), 0);
                game.newGame();
                string newGameMsg = "New game started!\n";
                send(clientSocket, newGameMsg.c_str(), newGameMsg.length(), 0);
            }
        }
        else { // Computer vs Computer
            pair<int, string> roundResult = game.playRound(ROCK);
            int roundWinner = roundResult.first;
            string result = roundResult.second;
            send(clientSocket, result.c_str(), result.length(), 0);
            Sleep(1000);

            if (game.isGameOver()) {
                string stats = game.showStats();
                send(clientSocket, stats.c_str(), stats.length(), 0);
                game.newGame();
                string newGameMsg = "New game started!\n";
                send(clientSocket = send(clientSocket, newGameMsg.c_str(), newGameMsg.length(), 0);
            }
        }
    }

    string goodbye = "Match over! Goodbye!\n";
    send(clientSocket, goodbye.c_str(), goodbye.length(), 0);

    closesocket(clientSocket);
    closesocket(serverSocket);
    WSACleanup();
    return 0;
}