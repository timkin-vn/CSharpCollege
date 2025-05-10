#include <iostream>
#include <curl/curl.h>
#include <vector>
#include <map>

size_t WriteCallback(void* contents, size_t size, size_t nmemb, std::string* output) {
    size_t total_size = size * nmemb;
    output->append((char*)contents, total_size);
    return total_size;
}

void searchEngine(const std::string& query, const std::string& engine) {
    CURL* curl;
    CURLcode res;
    std::string readBuffer;

    std::map<std::string, std::string> engines = {
        {"bing", "https://www.bing.com/search?q="},
        {"google", "https://www.google.com/search?q="},
        {"duckduckgo", "https://duckduckgo.com/?q="}
    };

    auto it = engines.find(engine);
    if (it == engines.end()) {
        std::cerr << "Unknown search engine: " << engine << std::endl;
        return;
    }

    curl = curl_easy_init();
    if (curl) {
        std::string url = it->second + query;
        curl_easy_setopt(curl, CURLOPT_URL, url.c_str());
        curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, WriteCallback);
        curl_easy_setopt(curl, CURLOPT_WRITEDATA, &readBuffer);
        curl_easy_setopt(curl, CURLOPT_FOLLOWLOCATION, 1L);

        res = curl_easy_perform(curl);
        if (res != CURLE_OK) {
            std::cerr << "curl_easy_perform() failed: " << curl_easy_strerror(res) << std::endl;
        }
        else {
            std::cout << "Results from " << engine << ":\n" << readBuffer.substr(0, 1000) << "...\n";
        }

        curl_easy_cleanup(curl);
    }
}

void searchImages(const std::string& query, const std::string& engine) {
    CURL* curl;
    CURLcode res;
    std::string readBuffer;

    std::map<std::string, std::string> engines = {
        {"bing", "https://www.bing.com/images/search?q="},
        {"google", "https://www.google.com/search?tbm=isch&q="},
        {"duckduckgo", "https://duckduckgo.com/?q=" + query + "&iax=images&ia=images"}
    };

    auto it = engines.find(engine);
    if (it == engines.end()) {
        std::cerr << "Unknown search engine: " << engine << std::endl;
        return;
    }

    curl = curl_easy_init();
    if (curl) {
        std::string url = it->second + query;
        curl_easy_setopt(curl, CURLOPT_URL, url.c_str());
        curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, WriteCallback);
        curl_easy_setopt(curl, CURLOPT_WRITEDATA, &readBuffer);
        curl_easy_setopt(curl, CURLOPT_FOLLOWLOCATION, 1L);

        res = curl_easy_perform(curl);
        if (res != CURLE_OK) {
            std::cerr << "curl_easy_perform() failed: " << curl_easy_strerror(res) << std::endl;
        }
        else {
            std::cout << "Image results from " << engine << ":\n" << readBuffer.substr(0, 1000) << "...\n";
        }

        curl_easy_cleanup(curl);
    }
}

int main() {
    std::string query, type;
    std::vector<std::string> engines = { "bing", "google", "duckduckgo" };

    std::cout << "Enter search query: ";
    std::getline(std::cin, query);

    std::cout << "Search type (web/images): ";
    std::getline(std::cin, type);

    for (const auto& engine : engines) {
        if (type == "images") {
            searchImages(query, engine);
        }
        else {
            searchEngine(query, engine);
        }
    }
    return 0;
}
