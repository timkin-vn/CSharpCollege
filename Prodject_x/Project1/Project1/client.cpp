// client.cpp
#include <iostream>
#include <string>
#include <winsock2.h>
#include <ws2tcpip.h>
#pragma comment(lib, "ws2_32.lib")

using namespace std;

int main() {
    WSADATA wsaData;
    WSAStartup(MAKEWORD(2, 2), &wsaData);

    SOCKET clientSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    sockaddr_in serverAddr;
    serverAddr.sin_family = AF_INET;
    serverAddr.sin_port = htons(8888);

    if (inet_pton(AF_INET, "127.0.0.1", &serverAddr.sin_addr) <= 0) {
        cout << "Invalid address/ Address not supported: " << WSAGetLastError() << endl;
        closesocket(clientSocket);
        WSACleanup();
        return 1;
    }

    if (connect(clientSocket, (sockaddr*)&serverAddr, sizeof(serverAddr)) == SOCKET_ERROR) {
        cout << "Connection failed: " << WSAGetLastError() << endl;
        closesocket(clientSocket);
        WSACleanup();
        return 1;
    }

    cout << "Connected to server\n";

    char buffer[1024];
    int bytesReceived = recv(clientSocket, buffer, sizeof(buffer), 0);
    if (bytesReceived > 0) {
        buffer[bytesReceived] = '\0';
        cout << buffer;
    }

    string mode;
    cout << "Enter mode (1 for Human vs Computer, 2 for Computer vs Computer): ";
    getline(cin, mode);
    send(clientSocket, mode.c_str(), mode.length(), 0);

    bytesReceived = recv(clientSocket, buffer, sizeof(buffer), 0);
    if (bytesReceived > 0) {
        buffer[bytesReceived] = '\0';
        cout << buffer;
    }

    while (true) {
        if (mode == "1") {
            string choice;
            cout << "Enter your choice (0-Rock, 1-Paper, 2-Scissors, 3-Lizard, 4-Spock, 5-Quit, 6-Draw): ";
            getline(cin, choice);
            send(clientSocket, choice.c_str(), choice.length(), 0);

            bytesReceived = recv(clientSocket, buffer, sizeof(buffer), 0);
            if (bytesReceived <= 0) break;
            buffer[bytesReceived] = '\0';
            cout << buffer;
        }
        else if (mode == "2") {
            bytesReceived = recv(clientSocket, buffer, sizeof(buffer), 0);
            if (bytesReceived <= 0) break;
            buffer[bytesReceived] = '\0';
            cout << buffer;
        }

        if (string(buffer).find("Match over") != string::npos) break;
    }

    closesocket(clientSocket);
    WSACleanup();
    return 0;
}