#include <string>
#include <vector>
#include <unordered_set>
#include <map>
#include <iostream>

using namespace std;

void Compair(map<int, int> c, unordered_set<int> b, int* count)
{
    if (c.size() == b.size())
    {
        (*count)++;
    }
}

int solution(vector<int> topping) {
    int answer = -1;
    int fairCount = 0;

    map<int, int> c;
    unordered_set<int> b;

    for (int el : topping)
    {
        if (c.find(el) == c.end())
        {
            c.insert({ el, 1 });
        }
        else
        {
            c[el]++;
        }
    }

    for (int to : topping)
    {
        b.insert(to);
        c[to]--;
        if (c[to] == 0)
        {
            c.erase(to);
        }

        Compair(c, b, &fairCount);
    }

    return fairCount;
}

int main()
{
    cout << solution({ 1,2,3,1,4}) << endl;
    //cout << solution({ 1,2,1,3,1,4,1,2 }) << endl;
    return 0;
}