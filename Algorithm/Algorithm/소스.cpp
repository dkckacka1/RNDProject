#include <string>
#include <vector>
#include <iostream>

using namespace std;

long DP[60000];
long recursion(long n);
int solution(int n);


int main()
{

    for (int i = 100; i >= 0; i--)
    {
        cout << solution(i) << endl;
    }

    return 0;
}

int solution(int n) 
{
    int answer = 0;
    DP[0] = 1;
    DP[1] = 2;
    return recursion(n - 1);
}

long recursion(long n)
{
    if (n == -1) return 0;

    if (DP[n] == 0)
    {
        DP[n] = recursion(n - 1) + recursion(n - 2);
    }

    return DP[n] % 10000000007;
}