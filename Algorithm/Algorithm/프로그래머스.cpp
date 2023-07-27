#ifdef 디펜스
#include<iostream>
#include <string>
#include <vector>
#include<queue>

using namespace std;
bool isDefense(int mid, int n, int k, vector<int>enemy);
int solution(int n, int k, vector<int> enemy)
{
	int answer = 0;

	int left = 0;
	int right = enemy.size();
	int mid = (left + right) / 2;

	while (left < right)
	{
		mid = (left + right) / 2;

		if (isDefense(mid, n, k, enemy))
		{
			left = mid + 1;
		}
		else
		{
			right = mid;
		}
	}

	answer = left;

	return answer;
}

bool isDefense(int mid, int n, int k, vector<int>enemy)
{
	priority_queue<int, vector<int>, greater<int>> pq;
	for (int i = 0; i < mid + 1; i++)
	{
		int enemys = enemy[i];
		pq.push(enemys);
	}

	while (pq.empty() != true)
	{
		int enemy = pq.top();

		if (n >= enemy)
		{
			n -= enemy;
			pq.pop();
		}
		else
		{
			if (k > 0)
			{
				--k;
				pq.pop();
			}
			else
			{
				return false;
			}
		}
	}

	return true;
}
int main()
{
	int n = 7;
	int k = 3;
	vector<int> enemy = { 4,2,4,5,3,3,1 };

	int answer = solution(n, k, enemy);

	cout << answer << endl;

	return 0;
}


#endif // 디펜스
#ifdef 점찍기

#include<iostream>
#include <string>
#include <vector>

using namespace std;

long long solution(int k, int d) {
	long long answer = 0;
	long long lk = (long long)k;
	long long ld = (long long)d;

	for (long long li = 0; li <= ld; li += lk)
	{
		cout << "-------------------------------" << endl;
		cout << "d : " << ld << " i : " << li << endl;
		long long max_y = sqrt((ld * ld) - (li * li));
		cout << "max_y : " << max_y << endl;
		cout << "(max_y / k) + 1 : " << (max_y / k) + 1 << endl;
		answer += (max_y / k) + 1;
	}

	return answer;
}

int main()
{
	int k = 1;
	int d = 5;
	long long answer = solution(k, d);
	cout << answer << endl;
	return 0;
}
#endif // 점찍기
#ifdef 귤나누기

#include<iostream>

#include <string>
#include <vector>
#include <queue>
using namespace std;
int tangerines[10000001];

int solution(int k, vector<int> tangerine)
{
	int answer = 0;
	priority_queue<int> pq;
	for (auto tan : tangerine)
	{
		tangerines[tan]++;
	}

	int maxsize = 0;

	for (int i = 1; i < 10000001; i++)
	{
		if (tangerines[i] == 0)
			continue;

		pq.push(tangerines[i]);
	}

	while (pq.empty() != true)
	{
		k -= pq.top();
		answer++;
		if (k <= 0)
			break;

		pq.pop();
	}

	return answer;
}

int main()
{
	int k = 4;
	vector<int> tangerine = { 1, 3, 2, 5, 4, 5, 2, 3 };

	cout << solution(k, tangerine);
	return 0;
}
#endif // 귤나누기
#ifdef 최대값과_최소값

#include<iostream>
#include <string>
#include <vector>
#include <sstream>
#include <algorithm>

using namespace std;

string solution(string s) {
	stringstream ss(s);

	vector<string> vs;
	vector<int> vi;

	string word;
	while (getline(ss, word, ' '))
	{
		vs.push_back(word);
	}

	for (auto word : vs)
	{
		vi.push_back(atoi(word.c_str()));
	}

	int max = *(max_element(vi.begin(), vi.end()));
	int min = *(min_element(vi.begin(), vi.end()));

	string answer = to_string(max) + " " + to_string(min);
	return answer;
}

int main()
{
	string s = "1 2 3 -4";
	cout << solution(s) << endl;
	return 0;
}
#endif // 최대값과_최소값
#ifdef JadenCase_문자열_만들기
#include<iostream>
#include <string>
#include <vector>
#include <sstream>
using namespace std;

string solution(string s)
{
	string answer = "";

	stringstream ss(s);
	vector<string> vs;
	string word;

	while (getline(ss, word, ' '))
	{
		for (int i = 0; i < word.size(); i++)
		{
			word[i] = tolower(word[i]);
		}

		word[0] = toupper(word[0]);
		vs.push_back(word);
	}

	for (int i = 0; i < vs.size(); i++)
	{
		answer += vs[i];

		if (i < vs.size() - 1)
			answer += " ";
	}

	for (int i = 0; i < s.size() - answer.size(); i++)
	{
		answer += " ";
	}

	return answer;
}

int main()
{
	string s = "3people   unFollowed me ";

	cout << solution(s) << ":" << endl;

	return 0;
}

#endif // JadenCase_문자열_만들기
#ifdef 최솟값만들기

#include <iostream>
#include<vector>
#include<algorithm>
using namespace std;

int solution(vector<int> A, vector<int> B)
{
	int answer = 0;

	sort(A.begin(), A.end(), std::less<int>());
	sort(B.begin(), B.end(), std::greater<int>());

	for (int i = 0; i < A.size(); i++)
	{
		answer += (A[i] * B[i]);
	}

	return answer;
}

int main()
{
	vector<int> A = { 1,2 };
	vector<int> B = { 3,4 };
	cout << solution(A, B) << endl;

	return 0;
}
#endif // 최솟값만들기
#ifdef 올바른괄호

#include<iostream>
#include<string>
#include <stack>

using namespace std;

bool solution(string s)
{
	stack<char> ss;

	for (int i = 0; i < s.size(); i++)
	{
		if (s[i] == '(')
		{
			ss.push(s[i]);
		}
		else if (s[i] == ')')
		{
			if (ss.empty() == true)
				return false;

			ss.pop();
		}
	}
	if (ss.empty() != true)
		return false;

	return true;
}
int main()
{
	string s = "(()(";

	cout << boolalpha;
	cout << solution(s) << endl;

	return 0;
}

#endif // 올바른괄호
#ifdef 이진_변환_반복하기

#include<iostream>
#include <string>
#include <vector>

using namespace std;

vector<int> solution(string s)
{
	vector<int> answer;
	int count = 0;
	int zeroCount = 0;

	string ns;

	while (ns.size() != 1)
	{

		ns.clear();
		for (int i = 0; i < s.size(); i++)
		{
			if (s[i] == '1')
			{
				ns += '1';
			}
			else if (s[i] == '0')
			{
				zeroCount++;
			}
		}

		int i = ns.size();
		s.clear();
		while (i > 0)
		{
			if (i % 2 == 0)
			{
				s += '0';
			}
			else
			{
				s += '1';
			}
			i /= 2;
		}
		count++;
	}

	answer.push_back(count);
	answer.push_back(zeroCount);

	return answer;
}
int main()
{
	string s = "1111111";
	vector<int> answer;

	answer = solution(s);

	for (auto i : answer)
		cout << i << " ";

	cout << endl;


	return 0;
}

#endif // 이진_변환_반복하기
#ifdef 숫자의_표현

#include<iostream>
#include <string>
#include <vector>

using namespace std;

int solution(int n) {
	int count = 0;
	int sum = 0;

	for (int i = 1; i <= n; i++)
	{
		for (int j = i; j <= n; j++)
		{
			sum += j;
			if (sum == n)
			{
				count++;
				break;
			}
			else if (sum > n)
			{
				break;
			}
			else
			{
				continue;
			}
		}
		sum = 0;
	}

	return count;
}

int main()
{
	cout << solution(18) << endl;

	return 0;
}
#endif // 숫자의_표현
#ifdef 피보나치_수

#include<iostream>
#include <string>
#include <vector>

using namespace std;

int f[100001];

int solution(int n)
{
	f[0] = 0;
	f[1] = 1;

	for (int i = 2; i <= n; i++)
	{
		f[i] = (f[i - 1] % 1234567 + f[i - 2] % 1234567) % 1234567;
	}

	return f[n];
}

int main()
{
	cout << solution(100000);
	return 0;
}
#endif // 피보나치_수
#ifdef 다음_큰_숫자

#include<iostream>
#include <string>

using namespace std;

int CountOne(int n)
{
	int oneCount = 0;

	while (n > 0)
	{
		if (n % 2 == 1)
		{
			oneCount++;
		}

		n /= 2;
	}

	return oneCount;
}

int solution(int n) {
	int oneCount = CountOne(n);

	while (true)
	{
		n++;
		if (oneCount == CountOne(n))
			break;
	}

	return n;
}

int main()
{
	cout << solution(78) << endl;

	return 0;
}
#endif // 다음_큰_숫자
#ifdef 카펫

#include<iostream>
#include <string>
#include <vector>

using namespace std;

vector<int> solution(int brown, int yellow) {
	vector<int> answer;

	for (int i = 3; i < 2500; i++)
	{
		for (int j = 3; j <= i; j++)
		{
			int cYellow = (i - 2) * (j - 2);
			int cBrown = (i * 2) + ((j - 2) * 2);

			if (cYellow == yellow && cBrown == brown)
			{
				answer.push_back(i);
				answer.push_back(j);

				return answer;
			}
		}
	}

	return answer;
}

int main()
{
	solution(10, 2);
	return 0;
}

#endif // 카펫
#ifdef 짝_지어_제거하기

#include<iostream>
#include<string>
#include<stack>
using namespace std;

int solution(string s)
{
	int answer = 0;
	stack<char> cs;

	for (int i = 0; i < s.size(); i++)
	{
		if (cs.empty() == true)
		{
			cs.push(s[i]);
			continue;
		}

		char top = cs.top();

		if (top == s[i])
			cs.pop();
		else
			cs.push(s[i]);
	}

	if (cs.empty() == true)
		answer = 1;

	return answer;
}

int main()
{
	cout << solution("baabaaa") << endl;

	return 0;
}
#endif // 짝_지어_제거하기
#ifdef 영어_끝말잇기

#include <string>
#include <vector>
#include <iostream>

using namespace std;

vector<int> solution(int n, vector<string> words) {
	vector<int> answer;

	int count = 0;
	int outNum = 0;

	vector<string> checkString;
	string check, preCheck;



	for (int i = 0; i < words.size(); i++)
	{
		outNum = (i % n) + 1;
		count = (i / n) + 1;
		check = words[i];
		auto iter = find(checkString.begin(), checkString.end(), check);

		if (checkString.empty() == false && iter != checkString.end())
		{
			answer.push_back(outNum);
			answer.push_back(count);

			return answer;
		}

		checkString.push_back(check);
		if (i != 0)
		{
			preCheck = words[i - 1];

			if (preCheck[preCheck.size() - 1] != check[0])
			{
				answer.push_back(outNum);
				answer.push_back(count);

				return answer;
			}
		}
	}

	answer.push_back(0);
	answer.push_back(0);

	return answer;
}

int main()
{
	int n = 3;
	vector<string> words = { "tank", "kick", "know", "wheel", "land", "dream", "mother", "robot", "tank" };

	vector<int> answer = solution(n, words);

	for (auto i : answer)
		cout << i << " ";

	return 0;
}
#endif // 영어_끝말잇기
#ifdef 구명보트

#include <iostream>
#include <string>
#include <vector>
#include <deque>
#include <algorithm>
using namespace std;

int solution(vector<int> people, int limit) {
	int count = 0;

	sort(people.begin(), people.end(), std::greater<int>());
	deque<int> d(people.begin(), people.end());

	for (auto i : d)
		cout << i << " ";

	int curLimit = 0;
	while (d.empty() != true)
	{
		count++;
		curLimit = 0;

		for (auto iter = d.begin(); iter != d.end();)
		{
			if (iter != d.end() && curLimit + *iter <= limit)
			{
				curLimit += *iter;
				iter++;
				d.pop_front();
				if (d.empty())
					iter = d.end();
				continue;
			}
			else
				break;

			iter++;
		}

		for (auto iter = d.rbegin(); iter != d.rend();)
		{
			if (iter != d.rend() && curLimit + *iter <= limit)
			{
				curLimit += *iter;
				iter++;
				d.pop_back();
				if (d.empty())
					iter = d.rend();
				continue;
			}
			else
				break;

			iter++;
		}
	}

	return count;
}

int main()
{
	vector<int> people = { 70, 50, 80, 50 };
	int limit = 100;

	cout << solution(people, limit) << endl;

	return 0;
}
#endif // 구명보트
#ifdef N개의_최소공배수


#include <iostream>
#include <string>
#include <vector>

using namespace std;

int gcd(int num1, int num2)
{
	if (num2 == 0)
		return num1;
	else
		return gcd(num2, num1 % num2);
}

int solution(vector<int> arr) {
	int answer = 0;

	if (arr.size() == 1)
		return arr[0];

	int gcdTemp = gcd(*(arr.begin()), *(arr.begin() + 1));
	int lcm = *(arr.begin()) * *(arr.begin() + 1) / gcdTemp;

	if (arr.size() == 2)
		return lcm;

	for (auto iter = arr.begin() + 2; iter != arr.end(); iter++)
	{
		gcdTemp = gcd(*iter, lcm);
		lcm = *iter * lcm / gcdTemp;
	}

	return lcm;
}

int main()
{
	vector<int> arr = { 1,2,3 };
	cout << solution(arr) << endl;
}
#endif // N개의_최소공배수
#ifdef 예상_대진표

#include <iostream>

using namespace std;

int Fight(int num)
{
	if (num % 2 == 0)
		return num / 2;
	else
		return (num + 1) / 2;
}

int solution(int n, int a, int b)
{
	int answer = 1;

	while (true)
	{
		if (a - b == -1 || a - b == 1)
			if ((a + b + 1) % 4 == 0)
				break;

		answer++;
		a = Fight(a);
		b = Fight(b);
	}

	return answer;
}

int main()
{
	int n = 8;
	int a = 4;
	int b = 7;

	cout << solution(n, a, b);

	return 0;
}

#endif // 예상_대진표
#ifdef 점프와_순간_이동

#include <iostream>
using namespace std;

int solution(int n)
{
	string s;
	while (n > 0)
	{
		if (n % 2 == 0)
			s.push_back('1');
		else
			s.push_back('0');

		n /= 2;
	}

	int count = 0;
	for (int i = 0; i < s.size(); i++)
	{
		if (s[i] == '0')
			count++;
	}

	return count;

}

int main()
{
	int n = 9;
	cout << solution(n) << endl;

	return 0;
}

#endif // 점프와_순간_이동
#ifdef 멀리뛰기

#include<iostream>
#include <string>

using namespace std;

int answers[2001];

int Solution(int n)
{
	if (n < 3 || answers[n] > 0)
		return answers[n];

	for (int i = 3; i < 2001; i++)
	{
		answers[i] = (answers[i - 1] % 1234567 + answers[i - 2] % 1234567) % 1234567;
		if (i >= n)
			break;
	}


	return answers[n];
}

long long solution(int n) {
	long long answer = 0;

	answers[1] = 1;
	answers[2] = 2;
	answers[3] = 3;

	return Solution(n);
}

int main()
{
	cout << solution(6) << endl;

	return 0;
}
#endif // 멀리뛰기
#ifdef H-Index

#include <string>
#include <vector>
#include<iostream>
#include <algorithm>

using namespace std;

int solution(vector<int> citations) {
	int max = 0;
	sort(citations.begin(), citations.end(), std::greater<int>());
	for (int i = 0; i < citations.size(); ++i) {
		if (citations[i] < i + 1) {
			return i;
		}
	}

	return citations.size();

	return max;
}

int main()
{
	vector<int> citations1 = { 10,8,5,4,3 };					// 4
	vector<int> citations2 = { 25,8,5,3,3 };					// 3
	vector<int> citations3 = { 15, 12, 10, 8, 6, 3, 2, 1 };		// 5
	vector<int> citations4 = { 0 };								// 0
	vector<int> citations5 = { 2 };								// 1
	vector<int> citations6 = { 3,6,5,0,1 };						// 3 

	cout << solution(citations1) << endl;
	cout << solution(citations2) << endl;
	cout << solution(citations3) << endl;
	cout << solution(citations4) << endl;
	cout << solution(citations5) << endl;
	cout << solution(citations6) << endl;

	return 0;
}


#endif // H-Index
#ifdef 카_1차_캐시
#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
#include <list>

using namespace std;

string ToLower(string str)
{
	string newstr;
	for (auto i : str)
	{
		if (i <= 'Z' && i >= 'A')
		{
			i = i - ('Z' - 'z');
		}
		newstr.push_back(i);
	}

	return newstr;
}

int solution(int cacheSize, vector<string> cities) {
	int answer = 0;
	if (cacheSize == 0)
		return cities.size() * 5;

	list<string> caches;
	string city = *(cities.begin());

	for (auto cIter = cities.begin(); cIter != cities.end(); cIter++)
	{
		city = ToLower(*(cIter));

		auto cache = find(caches.begin(), caches.end(), city);

		if (cache == caches.end())
		{
			caches.push_back(city);
			if (caches.size() > cacheSize)
			{
				caches.erase(caches.begin());
			}
			answer += 5;
		}
		else
		{
			caches.erase(cache);
			caches.push_back(city);
			answer++;
		}
	}

	return answer;
}

int main()
{
	int cacheSize = 3;
	vector<string> cities1 = { "Jeju", "Pangyo", "Seoul", "NewYork", "LA", "Jeju", "Pangyo", "Seoul", "NewYork", "LA" };
	vector<string> cities2 = { "Jeju", "Pangyo", "Seoul", "Jeju", "Pangyo", "Seoul", "Jeju", "Pangyo", "Seoul" };
	vector<string> cities3 = { "Jeju", "Pangyo", "Seoul", "NewYork", "LA", "SanFrancisco", "Seoul", "Rome", "Paris", "Jeju", "NewYork", "Rome" };
	vector<string> cities4 = { "Jeju", "Pangyo", "Seoul", "NewYork", "LA", "SanFrancisco", "Seoul", "Rome", "Paris", "Jeju", "NewYork", "Rome" };
	vector<string> cities5 = { "Jeju", "Pangyo", "NewYork", "newyork" };
	vector<string> cities6 = { "Jeju", "Pangyo", "Seoul", "NewYork", "LA" };
	vector<string> cities7 = { "A", "B", "A" };
	vector<string> cities8 = { "B", "C", "A", "B" };
	vector<string> cities9 = { "a", "a", "a", "b", "b", "b", "c", "c", "c" };

	cout << solution(3, cities1) << endl;
	cout << solution(3, cities2) << endl;
	cout << solution(2, cities3) << endl;
	cout << solution(5, cities4) << endl;
	cout << solution(2, cities5) << endl;
	cout << solution(0, cities6) << endl;
	cout << solution(3, cities7) << endl;
	cout << solution(5, cities8) << endl;
	cout << solution(2, cities9) << endl;
}

#endif // 1차_캐시
#ifdef 행렬의_곱


#include <iostream>
#include <string>
#include <vector>

using namespace std;

vector<vector<int>> solution(vector<vector<int>> arr1, vector<vector<int>> arr2) {
	vector<vector<int>> answer;

	for (int i = 0; i < arr1.size(); i++)
	{
		vector<int> v;
		for (int j = 0; j < arr2[0].size(); j++)
		{
			int sum = 0;
			for (int k = 0; k < arr1[0].size(); k++)
			{
				sum += arr1[i][k] * arr2[k][j];
			}
			v.push_back(sum);
		}
		answer.push_back(v);
	}

	return answer;
}
int main()
{
	vector<vector<int>> arr1 = { {1,4},{2,3},{4,1} };
	vector<vector<int>> arr2 = { {3,3},{3,3} };
	vector<vector<int>> arr3 = { {2,3,2},{4,2,4},{3,1,4} };
	vector<vector<int>> arr4 = { {5,4,3},{2,4,1},{3,1,4} };

	for (auto ele : solution(arr1, arr2))
	{
		cout << "{ ";
		for (auto i : ele)
		{
			cout << i << " ";
		}
		cout << "} ";
	}

	for (auto ele : solution(arr3, arr4))
	{
		cout << "{ ";
		for (auto i : ele)
		{
			cout << i << " ";
		}
		cout << "} ";
	}
	return 0;
}
#endif // 행렬의_곱
#ifdef 괄호_회전하기

#include<iostream>
#include <string>
#include <vector>
#include <stack>

using namespace std;

int solution(string s) {
	int answer = 0;

	for (int i = 0; i < s.size(); i++)
	{
		stack<char> cs;
		bool isCheck = false;
		for (int j = 0; j < s.size(); j++)
		{
			char c = s[j];
			if (c == '{' || c == '(' || c == '[')
			{
				cs.push(s[j]);
			}
			else
			{
				if (cs.empty() == true)
					break;

				if ((cs.top() == '[' && c == ']') ||
					(cs.top() == '{' && c == '}') ||
					(cs.top() == '(' && c == ')'))
					cs.pop();
				else
					break;
			}

			if (j == s.size() - 1)
				isCheck = true;
		}

		if (isCheck && cs.empty())
			answer++;

		s.push_back(*(s.begin()));
		s.erase(s.begin());
	}

	return answer;
}

int main()
{
	cout << solution("[") << endl;

	return 0;
}
#endif // 괄호_회전하기
#ifdef 위장

#include <iostream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

int solution(vector<vector<string>> clothes) {

	int answer = 0;
	vector<pair<string, vector<string>>> spy;

	for (auto i : clothes)
	{
		auto iter = spy.begin();
		for (; iter != spy.end(); iter++)
		{
			if ((*iter).first == i[1])
				break;
		}

		if (iter == spy.end())
		{
			vector<string> newV;
			newV.push_back(i[0]);
			newV.push_back("no_wear");
			spy.push_back(make_pair(i[1], newV));
		}
		else
		{
			(*iter).second.push_back(i[0]);
		}
	}

	for (auto i : spy)
	{
		if (answer == 0)
		{
			answer += i.second.size();
			continue;
		}
		answer *= i.second.size();
	}
	return answer - 1;
}


int main()
{
	vector<vector<string>> clothes = { {"yellow_hat", "headgear" }, { "blue_sunglasses", "eyewear" }, { "green_turban", "headgear" } };
	cout << solution(clothes) << endl;
	return 0;
}
#endif // 위장
#ifdef 기능개발

#include<iostream>
#include <string>
#include <vector>
#include <queue>

using namespace std;

vector<int> solution(vector<int> progresses, vector<int> speeds) {
	vector<int> answer;

	queue<pair<int, int>> q;

	for (int i = 0; i < progresses.size(); i++)
	{
		q.push(pair(progresses[i], speeds[i]));
	}

	int count = 0;

	while (q.empty() != true)
	{
		if (q.front().first < 100)
		{
			for (int i = 0; i < q.size(); i++)
			{
				q.front().first += q.front().second;
				q.push(pair(q.front().first, q.front().second));
				q.pop();
			}
		}
		else
		{
			for (int i = 0; i < q.size(); i++)
			{
				if (q.front().first < 100)
					break;

				q.pop();
				count++;
			}
			answer.push_back(count);
			count = 0;
		}
	}

	return answer;
}

int main()
{
	solution({ 93, 30, 55 }, { 1, 30, 5 });
	return 0;
}

#endif // 기능개발
#ifdef n^2_배열_자르기

#include<iostream>
#include <string>
#include <vector>

using namespace std;

vector<int> solution(int n, long long left, long long right) {
	vector<int> answer;
	for (int i = left; i <= right; i++)
	{
		answer.push_back(max((int)floor(i / n) + 1, (i % n) + 1));
	}

	return answer;
}

int main()
{

	solution(3, 2, 5);
	return 0;
}
#endif // n^2_배열_자르기
#ifdef 프린터

#include<iostream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

bool compare(pair<int, int> a, pair<int, int> b)
{
	return a.first < b.first;
}

int solution(vector<int> priorities, int location) {
	int answer = 0;
	vector<pair<int, int>> vp;
	for (auto i : priorities)
		vp.push_back(pair(i, answer++));

	answer = 0;
	while (vp.empty() != true)
	{

		auto max_iter = max_element(vp.begin(), vp.end(), compare);
		int max_index = distance(vp.begin(), max_iter);
		for (int i = 0; i < max_index; i++)
		{
			vp.push_back(*vp.begin());
			vp.erase(vp.begin());
		}

		auto cur_p = vp.begin();
		answer++;
		if ((*cur_p).second == location)
			return answer;

		vp.erase(cur_p);
	}


	return answer;
}

int main()
{

	vector<int> priorities = { 1, 1, 9, 1, 1, 1 };

	cout << solution(priorities, 0);
	return 0;
}
#endif // 프린터
#ifdef _1차_뉴스_클러스터링

#include<iostream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

int Jaccard(int intersectionS, int unionS)
{
	return (intersectionS * 65536) / (unionS);
}

int solution(string str1, string str2) {
	int answer = 0;
	vector<string> vs1, vs2;
	double j;
	string word;
	for (int i = 0; i < str1.size() - 1; i++)
	{
		if (isalpha(str1[i]) == false || isalpha(str1[i + 1]) == false)
			continue;

		word.push_back(toupper(str1[i]));
		word.push_back(toupper(str1[i + 1]));
		vs1.push_back(word);
		word.clear();
	}

	for (int i = 0; i < str2.size() - 1; i++)
	{
		if (isalpha(str2[i]) == false || isalpha(str2[i + 1]) == false)
			continue;

		word.push_back(toupper(str2[i]));
		word.push_back(toupper(str2[i + 1]));
		vs2.push_back(word);
		word.clear();
	}

	{
		for (auto i : vs1)
			cout << i << " ";
		cout << endl;

		for (auto i : vs2)
			cout << i << " ";
		cout << endl;

	}

	vector<string> intersection_v, union_v; // 교집합, 합집합

	union_v.insert(union_v.end(), vs1.begin(), vs1.end());
	union_v.insert(union_v.end(), vs2.begin(), vs2.end());

	for (auto iter = vs1.begin(); iter != vs1.end();)
	{
		auto find_iter = find(vs2.begin(), vs2.end(), *iter);
		if (find_iter != vs2.end())
		{
			intersection_v.push_back(*iter);
			iter = vs1.erase(iter);
			vs2.erase(find_iter);
			continue;
		}
		iter++;
	}

	for (auto i : intersection_v)
	{
		auto find_iter = find(union_v.begin(), union_v.end(), i);
		if (find_iter != union_v.end())
			union_v.erase(find_iter);
	}
	{
		cout << "합집합 : ";
		for (auto i : union_v)
			cout << i << " ";
		cout << "합집합 사이즈 : " << union_v.size();
		cout << endl;

		cout << "교집합 : ";
		for (auto i : intersection_v)
			cout << i << " ";
		cout << "교집합 사이즈 : " << intersection_v.size();
		cout << endl;
	}


	if (union_v.empty())
	{
		cout << 65536 << endl;
		return 65536;
	}

	answer = Jaccard(intersection_v.size(), union_v.size());
	cout << answer << endl;
	return answer;
}

int main()
{
	solution("handshake", "shake hands");
	return 0;
}
#endif // [1차] 뉴스 클러스터링
#ifdef 전화번호부
include <iostream>
#include <string>
#include <vector>
#include <set>

using namespace std;

bool solution(vector<string> phone_book) {
	set<string> s_set;
	for (auto& number : phone_book)
		s_set.insert(number);
	for (auto iter1 = s_set.begin(); iter1 != s_set.end(); ++iter1)
	{
		for (auto iter2 = iter1; iter2 != s_set.end(); ++iter2)
		{
			if (iter1 != iter2)
			{
				if (iter2->find(*iter1) == 0)
					return false;
				else
					break;
			}
		}
	}
	return true;
}

int main()
{
	vector<string> phone_Book1 = { "119", "97674223", "1195524421" };
	vector<string> phone_Book2 = { "123","456","789" };
	vector<string> phone_Book3 = { "12","123","1235","567","88" };
	cout << boolalpha;
	cout << solution(phone_Book1) << endl;
	cout << solution(phone_Book2) << endl;
	cout << solution(phone_Book3) << endl;
	return 0;
}
#endif // 전화번호부
#ifdef 타겟넘버

#include <iostream>
#include <string>
#include <vector>
#include <queue>

using namespace std;

int solution(vector<int> numbers, int target) {
	int count = 0;
	queue<pair<int, int>> qs; // 순서, 결과

	qs.push(pair(0, numbers[0]));
	qs.push(pair(0, -numbers[0]));
	while (qs.empty() != true)
	{
		auto topPair = qs.front();

		if (topPair.first < numbers.size() - 1)
		{
			qs.push(pair(topPair.first + 1, topPair.second + numbers[topPair.first + 1]));
			qs.push(pair(topPair.first + 1, topPair.second - numbers[topPair.first + 1]));
			qs.pop();
		}
		else
		{
			if (topPair.second == target)
				count++;
			qs.pop();
			continue;
		}
	}

	cout << endl;
	return count;
}

int main()
{
	vector<int> numbers1 = { 1, 1, 1, 1, 1 };
	vector<int> numbers2 = { 4, 1, 2, 1 };

	cout << solution(numbers1, 3) << endl;
	cout << solution(numbers2, 4) << endl;
}
#endif // 타겟넘버
#ifdef k진수에서_소수_개수_구하기


#include <iostream>
#include <string>
#include <vector>
#include <sstream>
#include <stack>

using namespace std;

bool isPrimeNumber(long long num)
{
	if (num == 1 || num == 0 || !num)
		return false;

	for (long long i = 2; i * i <= num; i++)
	{
		if (num % i == 0)
			return false;
	}
	return true;
}

int solution(int n, int k) {
	/*
		진수 변환
	*/
	string str;
	int mok = 2147483647;
	int namerge;
	stack<int> s;
	int ch;

	while (1)
	{
		if (mok == 0)
			break;

		if (mok == 2147483647)
		{
			mok = n / k;
			namerge = n % k;
		}
		else
		{
			namerge = mok % k;
			mok = mok / k;
		}
		s.push(namerge);

	}
	for (int i = 0; s.size(); i++)
	{
		ch = s.top();
		str.push_back(ch + 48);
		s.pop();
	}
	cout << str << endl;

	/*
		조건 찾기
	*/
	stringstream ss(str);
	vector<long long> vll;
	string index;
	while (getline(ss, index, '0'))
	{
		vll.push_back(atoll(index.c_str()));
	}

	for (auto i : vll)
		cout << i << " ";
	cout << endl;

	/*
		소수 확인
	*/
	vector<long long> pnv;

	for (auto i : vll)
	{
		if (isPrimeNumber(i))
			pnv.push_back(i);
	}

	for (auto i : pnv)
		cout << i << " ";
	cout << endl;

	return pnv.size();
}

int main()
{
	cout << solution(797161, 3) << endl;

	return 0;
}

#endif // k진수에서_소수_개수_구하기
#ifdef _3차_압축
#include<iostream>
#include <string>
#include <vector>
#include <unordered_map>
#include <algorithm>

using namespace std;

bool compare(pair<string, int> a, pair<string, int > b)
{
	return a.second > b.second;
}

vector<int> solution(string msg)
{
	vector<int> answer;
	unordered_map<string, int> m;
	int last_num = 26;
	for (int i = 1; i <= 26; i++)
	{
		char ch = i + 64;
		string str;
		str.push_back(ch);
		m.insert(pair(str, i));
	}

	while (msg.empty() != true)
	{
		string find_n_gram, nFind_n_gram;
		for (int i = 0; i < msg.size(); i++)
		{
			nFind_n_gram.push_back(msg[i]);
			auto findIter = m.find(nFind_n_gram);
			if (findIter == m.end())
			{
				m.insert(pair(nFind_n_gram, ++last_num));
				break;
			}
			else
			{
				find_n_gram = nFind_n_gram;
			}
		}
		answer.push_back(m.find(find_n_gram)->second);
		msg.erase(0, find_n_gram.size());
	}

	//for (auto i : m)
	//{
	//	cout << i.first << " : " << i.second << endl;
	//}
	//cout << "-----------------------------------------" << endl;
	for (auto i : answer)
		cout << i << " ";
	cout << endl;
	return answer;
}

int main()
{
	solution("KAKAO");
	cout << "-----------------------------------------" << endl;
	solution("TOBEORNOTTOBEORTOBEORNOT");
	cout << "-----------------------------------------" << endl;
	solution("ABABABABABABABAB");
	return 0;
}

#endif // _3차_압축
#ifdef 더_맵게
#include<iostream>
#include <string>
#include <vector>
#include <queue>

using namespace std;

int solution(vector<int> scoville, int K)
{
	int count = 0;

	priority_queue<int, vector<int>, std::greater<int>> pq(scoville.begin(), scoville.end());

	while (pq.empty() != true)
	{
		int low_scoville = pq.top();

		if (low_scoville >= K)
			break;
		else
			count++;

		pq.pop();
		if (pq.empty() == true)
			return -1;
		int next_scoville = pq.top();
		pq.pop();

		int mix_scoville = low_scoville + (next_scoville * 2);
		pq.push(mix_scoville);
	}

	return count;
}

int main()
{
	cout << solution({ 1, 2, 3, 9, 10, 12 }, 7);
	return 0;
}
#endif // 더_맵게
#ifdef 주차_요금_계산
#include<iostream>
#include <string>
#include <vector>
#include <map>
#include <sstream>
#include <algorithm>
using namespace std;

int CheckTime(string a, string b)
// a : 입차 , b : 출차
{
	string str;
	str.push_back(a[0]);
	str.push_back(a[1]);
	int ah = atoi(str.c_str());
	str.clear();
	str.push_back(a[3]);
	str.push_back(a[4]);
	int am = atoi(str.c_str());
	str.clear();
	str.push_back(b[0]);
	str.push_back(b[1]);
	int bh = atoi(str.c_str());
	str.clear();
	str.push_back(b[3]);
	str.push_back(b[4]);
	int bm = atoi(str.c_str());
	int time = (bh * 60 + bm) - (ah * 60 + am);
	return time;
}

vector<int> solution(vector<int> fees, vector<string> records)
{
	vector<int> answer;
	vector<pair<string, string>> parkingStation; // 차번, 입차 시간
	map<string, int> m; // 차번, 금액
	for (auto i : records)
	{
		vector<string> vs;
		stringstream ss(i);
		string index;
		while (getline(ss, index, ' '))
		{
			vs.push_back(index);
			// 0 : 시간
			// 1 : 차번
			// 2 : 입출차
		}

		if (vs[2] == "IN")
			// 입차
		{
			m.insert(pair(vs[1], 0));
			parkingStation.push_back(pair(vs[1], vs[0]));
		}
		else
			// 출차
		{
			string outCar = vs[1];
			auto lambda = [outCar](pair<string, string> c) {return c.first == outCar; };
			vector<pair<string, string>>::iterator it = find_if(parkingStation.begin(), parkingStation.end(), lambda); // 주차장내의 해당 차번 찾기
			int checkTime;
			string in = it->second; // 입차
			string out = vs[0]; // 출차
			m.find(outCar)->second += CheckTime(in, out);
			parkingStation.erase(it);

		}
	}

	for (auto iter = parkingStation.begin(); iter != parkingStation.end(); iter++)
	{
		string in = iter->second; // 입차
		string out = "23:59"; // 출차
		m.find(iter->first)->second += CheckTime(in, out);
	}

	for (auto i : m)
		cout << i.first << " : " << i.second << '\t';
	cout << endl;

	for (auto i : m)
	{
		int fee = 0;
		if (i.second < fees[0])
		{
			answer.push_back(fees[1]);
			continue;
		}
		int time = i.second;
		int unitTime = (time - fees[0]) % fees[2] == 0 ? (time - fees[0]) / fees[2] : ((time - fees[0]) / fees[2]) + 1;
		fee = fees[1] + (unitTime * fees[3]);
		answer.push_back(fee);
	}

	for (auto i : answer)
		cout << i << " ";
	cout << endl;
	return answer;
}

int main()
{
	for (auto i : solution({ 180, 5000, 10, 600 }, { "05:34 5961 IN", "06:00 0000 IN", "06:34 0000 OUT", "07:59 5961 OUT", "07:59 0148 IN", "18:59 0000 IN", "19:09 0148 OUT", "22:59 5961 IN", "23:00 5961 OUT" }))
		cout << i << " ";
	cout << endl << "----------------------------------" << endl;
	for (auto i : solution({ 120, 0, 60, 591 }, { "16:00 3961 IN","16:00 0202 IN","18:00 3961 OUT","18:00 0202 OUT","23:58 3961 IN" }))
		cout << i << " ";
	cout << endl << "----------------------------------" << endl;
	for (auto i : solution({ 1, 461, 1, 10 }, { "00:00 1234 IN" }))
		cout << i << " ";
	cout << endl << "----------------------------------" << endl;
	return 0;
}
#endif // 주차_요금_계산
#ifdef 오픈채팅방
#include <string>
#include <vector>
#include <map>
#include <sstream>
#include <algorithm>

using namespace std;

vector<string> solution(vector<string> record)
{
	map<string, string> m; // ID, 닉네임
	vector<pair<string, string>> vp; // ID, 명령
	vector<string> answer; // 대화방내용

	for (auto i : record)
	{
		stringstream ss(i);
		vector<string> vs;
		string index;
		while (getline(ss, index, ' '))
		{
			vs.push_back(index); // 0: 명령, 1: ID, 2: 닉네임
		}

		if (vs[0] == "Change")
		{
			m[vs[1]] = vs[2];
			continue;
		}
		else if (vs[0] == "Leave")
		{
			vp.push_back(pair(vs[1], vs[0]));
			continue;
		}
		else
		{
			vp.push_back(pair(vs[1], vs[0]));
		}

		string id = vs[1];
		m[vs[1]] = vs[2];
	}

	string str;
	for (auto i : vp)
	{
		str += m[i.first] + "님이";
		if (i.second == "Enter")
			str += " 들어왔습니다.";
		else
			str += " 나갔습니다.";
		answer.push_back(str);
		str.clear();
	}

	return answer;
}

int main()
{
	solution({ "Enter uid1234 Muzi", "Enter uid4567 Prodo","Leave uid1234","Enter uid1234 Prodo","Change uid4567 Ryan" });

	return 0;
}
#endif // 오픈채팅방
#ifdef 피로도
#include <iostream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

int solution(int k, vector<vector<int>> dungeons) {
	int maxClear = 0;
	sort(dungeons.begin(), dungeons.end());
	while (next_permutation(dungeons.begin(), dungeons.end()))
	{
		int clear = 0;
		int piro = k;
		for (auto iter = dungeons.begin(); iter != dungeons.end(); iter++)
		{
			if (piro >= (*iter)[0])
			{
				clear++;
				piro -= (*iter)[1];
			}
		}
		maxClear = max(clear, maxClear);
	}

	return maxClear;
}

int main()
{
	cout << solution(80, { {80, 20},{50, 40},{30, 10} }) << endl;
	cout << "---------------------------" << endl;
}
#endif // 피로도
#ifdef 주식투자
#include <string>
#include <vector>
#include <stack>
#include <iostream>

using namespace std;

vector<int> solution(vector<int> prices) {
	vector<int> answer(prices.size());
	stack<int> s;
	int size = prices.size(); // 계속 size를 계산하는 것보다 상수값으로 저장하면 전체 함수 처리 시간 감소

	for (int i = 0; i < size; ++i) {
		while (!s.empty()
			&& prices[s.top()] > prices[i])
		{
			// 가격이 줄어들었다면
			answer[s.top()] = i - s.top(); // 현재 시간 - 당시 시간
			s.pop();
		}
		s.push(i);
	}
	while (!s.empty())
	{
		answer[s.top()] = size - 1 - s.top(); // 종료 시간 - 당시 시간
		s.pop();
	}

	for (auto i : answer)
		cout << i << " ";

	return answer;
}

int main()
{
	solution({ 1, 2, 3, 2, 3 });
	return 0;
}
#endif // 주식투자
#ifdef n진수_게임

#include <string>
#include <vector>
#include <stack>
#include <iostream>

using namespace std;

string jin(int n, int k)
{
	string str;
	int mok = 2147483647;
	int namerge;
	stack<char> s;
	int ch;

	while (1)
	{
		if (mok == 0)
			break;

		if (mok == 2147483647)
		{
			mok = n / k;
			namerge = n % k;
		}
		else
		{
			namerge = mok % k;
			mok = mok / k;
		}
		switch (namerge)
		{
		case 10:  s.push('A'); break;
		case 11:  s.push('B'); break;
		case 12:  s.push('C'); break;
		case 13:  s.push('D'); break;
		case 14:  s.push('E'); break;
		case 15:  s.push('F'); break;
		default: s.push(namerge + 48); break;
		}
	}

	for (int i = 0; s.size(); i++)
	{
		ch = s.top();
		str.push_back(ch);
		s.pop();
	}
	//cout << str << endl;
	return str;
}

string solution(int n, int t, int m, int p) {
	// 진법 n, 미리 구할 숫자의 갯수 t, 게임에 참가하는 인원 m, 튜브의 순서 p
	string answer = "";
	vector<int> v;
	for (int i = 0; i < t; i++)
		v.push_back(p + i * m);

	int index = 0;
	string s = jin(index++, n);
	for (auto i : v)
	{
		while (i > s.size())
			s += jin(index++, n);

		answer.push_back(s[i - 1]);
	}

	cout << answer << endl;
	return answer;
}

int main()
{
	solution(2, 4, 2, 1);

	return 0;
}
#endif // n진수_게임
#ifdef 연속_부분_수열_합의_개수
#include <iostream>
#include <string>
#include <vector>
#include <numeric>
#include <map>

using namespace std;

int solution(vector<int> elements) {
	vector<int> v(elements);
	v.insert(v.end(), elements.begin(), elements.end());
	map<int, int> m;
	for (int i = 1; i <= elements.size(); i++)
	{
		for (int j = 0; j < elements.size(); j++)
		{
			int sum = accumulate(v.begin() + j, v.begin() + j + i, 0);
			m.insert(pair(sum, 0));
			cout << sum << endl;
		}
	}

	cout << endl << endl << m.size();

	return m.size();
}

int main()
{
	solution({ 7,9,1,1,4 });
	return 0;
}
#endif // 연속_부분_수열_합의_개수
#ifdef 스킬트리

#include <iostream>
#include <string>
#include <vector>

using namespace std;

int solution(string skill, vector<string> skill_trees) {
	int answer = 0;

	for (auto user_skill : skill_trees)
	{
		int index = user_skill.size() - 1;
		bool isCheck = false;
		bool isRight = true;
		for (int i = skill.size() - 1; i >= 0; i--)
		{
			char last_skill = skill[i];
			for (int j = index; j >= 0; j--)
			{
				if (last_skill == user_skill[j])
				{
					index = j;
					isCheck = true;
					break;
				}

				if (j == 0 && isCheck)
					isRight = false;
			}
		}

		answer += isRight;
	}

	cout << answer;
	return answer;
}

int main()
{
	solution("CBD", { "BACDE", "CBADF", "AECB", "BDA" , "HCJ" , "CBD" , "D" });
}
#endif // 스킬트리
#ifdef 땅따먹기
#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

int dp[100][100];
int max_ele(int a, int b, int c);

int solution(vector<vector<int>> land)
{
	int answer = 0;
	int earse_index = -1;


	for (int i = 1; i < land.size(); i++)
	{
		land[i][0] += max_ele(land[i - 1][1], land[i - 1][2], land[i - 1][3]);
		land[i][1] += max_ele(land[i - 1][0], land[i - 1][2], land[i - 1][3]);
		land[i][2] += max_ele(land[i - 1][0], land[i - 1][1], land[i - 1][3]);
		land[i][3] += max_ele(land[i - 1][0], land[i - 1][1], land[i - 1][2]);
	}

	answer = *max_element(land[land.size() - 1].begin(), land[land.size() - 1].end());

	cout << answer;
	return answer;
}

int max_ele(int a, int b, int c)
{
	return max(max(a, b), c);
}

int main()
{
	solution({ {1,2,3,5},{5,6,7,8},{4,3,2,1} });
}
#endif // 땅따먹기
#ifdef 방문_길이
#include <iostream>
#include <string>
#include <set>
using namespace std;

int solution(string dirs) {

	set < pair<pair<int, int>, pair<int, int>>> s;
	pair<int, int> current_location = pair(0, 0);

	for (int i = 0; i < dirs.size(); i++)
	{
		switch (dirs[i])
		{
		case'L':
		{
			if (current_location.first <= -5)
				break;

			pair<int, int> next_location = pair(current_location.first - 1, current_location.second);
			s.insert(pair(current_location, next_location));
			s.insert(pair(next_location, current_location));
			current_location = next_location;
			break;
		}
		case'R':
		{
			if (current_location.first >= 5)
				break;

			pair<int, int> next_location = pair(current_location.first + 1, current_location.second);
			s.insert(pair(current_location, next_location));
			s.insert(pair(next_location, current_location));
			current_location = next_location;
			break;
		}
		case'U':
		{
			if (current_location.second >= 5)
				break;

			pair<int, int> next_location = pair(current_location.first, current_location.second + 1);
			s.insert(pair(current_location, next_location));
			s.insert(pair(next_location, current_location));
			current_location = next_location;
			break;
		}
		case'D':
		{
			if (current_location.second <= -5)
				break;

			pair<int, int> next_location = pair(current_location.first, current_location.second - 1);
			s.insert(pair(current_location, next_location));
			s.insert(pair(next_location, current_location));
			current_location = next_location;
			break;
		}
		}
	}

	return s.size() / 2;
}

int main()
{

	cout << solution("LULLLLLLU");
	return 0;
}
#endif // 방문_길이
#ifdef 프렌즈4블록
#include <iostream>
#include <string>
#include <vector>

using namespace std;

int boardInt[30][30];

int CheckBoard(vector<string>& board);
void CheckBlock(int y, int x, vector<string>& board);
void DownBlock(vector<string>& board);
void Swap(char& a, char& b);

int solution(int m, int n, vector<string> board) {
	int pre_clearBlockCount, clearBlockCount = 0;
	while (true)
	{
		pre_clearBlockCount = clearBlockCount;
		clearBlockCount += CheckBoard(board);
		if (pre_clearBlockCount == clearBlockCount)
			break;
		else
			DownBlock(board);
	}

	return clearBlockCount;
}

int CheckBoard(vector<string>& board)
{
	for (int i = 0; i < board.size() - 1; i++)
	{
		for (int j = 0; j < board[0].size() - 1; j++)
		{
			if (board[i][j] != '-')
				CheckBlock(i, j, board);
		}
	}

	int count = 0;

	for (int i = 0; i < board.size(); i++)
	{
		for (int j = 0; j < board[0].size(); j++)
		{
			if (boardInt[i][j] == 1)
			{
				boardInt[i][j] = 0;
				board[i][j] = '-';
				count++;
			}
		}
	}

	return count;
}

void CheckBlock(int y, int x, vector<string>& board)
{
	if (board[y][x] == board[y][x + 1] &&
		board[y][x] == board[y + 1][x] &&
		board[y][x] == board[y + 1][x + 1])
	{
		boardInt[y][x] = 1;
		boardInt[y][x + 1] = 1;
		boardInt[y + 1][x] = 1;
		boardInt[y + 1][x + 1] = 1;
	}
}

void DownBlock(vector<string>& board)
{
	for (int i = board.size() - 1; i >= 0; i--)
	{
		for (int j = board[0].size() - 1; j >= 0; j--)
		{
			if (board[i][j] == '-')
				for (int k = i; k >= 0; k--)
				{
					if (board[k][j] != '-')
					{
						Swap(board[k][j], board[i][j]);
						break;
					}
				}
		}
	}
}

void Swap(char& a, char& b)
{
	char temp = a;
	a = b;
	b = temp;
}

int main()
{
	cout << solution(4, 5, {
"DDABBAABBA",
"AAAAAABBBA",
"DDACCBBBAA",
"DDABBBBBAA",
"AAABBABBBA",
"CCADDAABBB",
"CCADDAABBB",
"BBACCABBBA",
"BBAAABBBAA",
"DDABBBBAAA" });

	return 0;
}
#endif // 프렌즈4블록
#ifdef 파일명_정렬
#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
using namespace std;

struct Filename
{
	string head;
	int number;
	int index;

	void Print()
	{
		cout << head << number << index << endl;
	}
};

vector<Filename> v;
bool isNumber(char c)
{
	if (c >= '0' && c <= '9') {
		return true;
	}
	return false;
}

bool cmp(Filename a, Filename b)
{
	if (a.head == b.head)
	{
		if (a.number == b.number)
		{
			return a.index < b.index;
		}
		else
			return a.number < b.number;
	}

	return a.head < b.head;

}

vector<string> solution(vector<string> files) {
	vector<string> answer;


	for (int i = 0; i < files.size(); i++)
	{
		string s = files[i];
		Filename f;

		for (int j = 0; j < s.size(); j++)
		{
			if (isdigit(s[j]))
			{
				f.head = s.substr(0, j);
				s = s.substr(j);

				for (int k = 0; k < f.head.size(); k++)
					f.head[k] = toupper(f.head[k]);

				break;
			}
		}




		for (int j = 0; j < s.size(); j++)
		{
			if (!isdigit(s[j]))
			{
				f.number = stoi(s.substr(0, j));
				break;
			}

			if (j == s.size() - 1)
				f.number = stoi(s);

		}

		f.index = i;
		cout << files[i] << " ==> ";
		cout << f.head << ", " << f.number << ", " << f.index << endl;

		v.push_back(f);
	}
	for (auto i : v)
		i.Print();
	cout << "-------------------------------" << endl;
	sort(v.begin(), v.end(), cmp);
	for (auto i : v)
		i.Print();

	for (auto i : v)
		answer.push_back(files[i.index]);

	cout << "-------------------------------" << endl;
	for (auto i : answer)
		cout << i << endl;

	return answer;
}
int main()
{
	//DivisionString("img12.png");
	solution({ "img12.png", "img10.png",   "IMG01.GIF", "img2.JPG","MUZI01.zip","img02.png","muzi1.txt","MUZI1.txt","muzi001.txt","img1.png","muzi1.TXT","foo9.txt","foo010bar020.zip","F-15" });
	//solution({ "F-5 Freedom Fighter", "B-50 Superfortress", "A-10 Thunderbolt II", "F-14 Tomcat" });
	return 0;
}
#endif // 파일명_정렬
#ifdef 할인_행사
#include<iostream>
#include <string>
#include <vector>
#include <deque>
#include <algorithm>

using namespace std;

int solution(vector<string> want, vector<int> number, vector<string> discount)
{
	int answer = 0;
	deque<string> d, dis;


	for (int i = 0; i < 10; i++)
		d.push_back(discount[i]);
	for (int i = 10; i < discount.size(); i++)
		dis.push_back(discount[i]);

	while (true)
	{

		for (int i = 0; i < want.size(); i++)
		{
			if (number[i] == count(d.begin(), d.end(), want[i]))
			{
				if (i == want.size() - 1)
					answer++;
			}
			else
			{
				break;
			}
		}

		if (dis.empty())
		{
			break;
		}

		d.pop_front();
		d.push_back(dis.front());
		dis.pop_front();
	}

	return answer;
}

int main()
{
	cout << solution({ "banana", "apple", "rice", "pork", "pot" }, { 3, 2, 2, 2, 1 }, { "chicken", "apple", "apple", "banana", "rice", "apple", "pork", "banana", "pork", "rice", "pot", "banana", "apple", "banana" });
	cout << solution({ "apple" }, { 10 }, { "banana", "banana", "banana", "banana", "banana", "banana", "banana", "banana", "banana", "banana" });
}
#endif // DEBUG
#ifdef 모음사전
#include <iostream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

char returnChar(char c)
{
	switch (c)
	{
	case 'A':
		return 'E';
	case 'E':
		return 'I';
	case 'I':
		return 'O';
	case 'O':
		return 'U';
	case 'U':
		return ' ';
	case ' ':
		return 'A';
	}
}

int solution(string word) {
	int answer = 0;
	string str;
	while (word != str)
	{
		//cout << str << endl;
		answer++;
		if (str.size() < 5)
		{
			str.push_back('A');
			continue;
		}

		for (int i = 4; i >= 0; i--)
		{
			str[i] = returnChar(str[i]);
			if (str[i] == ' ')
			{
				str.pop_back();
				continue;
			}

			break;
		}

	}

	return answer;
}

int main()
{
	cout << solution("AAAAE");
	cout << endl;
	cout << solution("AAAE");
	cout << endl;
	cout << solution("I");
	cout << endl;
	cout << solution("EIO");
	cout << endl;
}
#endif // 모음사전
#ifdef 게임_맵_최단거리
#include <iostream>

#include <vector>
#include <queue>    
using namespace std;

int board[101][101];
int dist[101][101];
int dx[4] = { 0, 1, -1, 0 };
int dy[4] = { 1, 0, 0, -1 };
queue<pair<int, int>> Q;

int solution(vector<vector<int> > maps)
{
	int answer = 0;
	for (int i = 0; i < 101; i++)
		fill(dist[i], dist[i] + 101, 0);
	for (int i = 0; i < maps.size(); i++)
		for (int j = 0; j < maps[i].size(); j++)
			board[i][j] = maps[i][j];

	int verticalCount = maps.size();
	int horizontalCount = maps[0].size();

	Q.push({ 0, 0 });
	dist[0][0] = 1;
	while (!Q.empty())
	{
		auto cur = Q.front();
		Q.pop();
		for (int i = 0; i < 4; i++)
		{
			int nx = dx[i] + cur.first;
			int ny = dy[i] + cur.second;
			if (nx < 0 || ny < 0 || nx >= verticalCount || ny >= horizontalCount)
				continue;
			if (board[nx][ny] != 1 || dist[nx][ny] >= 1)
				continue;
			Q.push({ nx, ny });
			dist[nx][ny] = dist[cur.first][cur.second] + 1;
		}
	}

	if (dist[verticalCount - 1][horizontalCount - 1] == 0)
		return -1;
	else
		return dist[verticalCount - 1][horizontalCount - 1];

	return answer;
}

int main()
{

	cout << solution({
		{1, 0, 1, 1, 1},
		{1, 0, 1, 0, 1},
		{1, 0, 1, 1, 1},
		{1, 1, 1, 0, 1},
		{0, 0, 0, 0, 1},
		{0, 0, 0, 0, 1},
		{0, 0, 0, 0, 1} ,
		{0, 0, 0, 0, 1} });
	cout << endl << "===========================================" << endl;
	cout << solution({
		{1, 0, 1, 1, 1},
		{1, 0, 1, 0, 1},
		{1, 0, 1, 1, 1},
		{1, 1, 1, 0, 0},
		{0, 0, 0, 0, 1} });

	return 0;
}
#endif // 게임_맵_최단거리
#ifdef 다리를_지나는_트럭
#include <iostream>
#include <string>
#include <vector>
#include <queue>

using namespace std;

int solution(int bridge_length, int weight, vector<int> truck_weights)
{
	int answer = 0;

	vector<pair<int, int>> v; // 무게, 시간
	queue<int> tq;
	for (auto i : truck_weights)
		tq.push(i);

	int w = 0;;

	while (true)
	{
		answer++;
		for (int i = 0; i < v.size(); i++)
		{
			//cout << "[" << v[i].first << ", " << v[i].second << "], ";
			v[i].second--;
			if (v[i].second <= 0)
				w -= v[i].first;
		}
		//cout << endl;

		auto rambda = [w](pair<int, int> p)
		{
			//cout << p.first << " : " << p.second << endl;
			return p.second == 0;
		};
		v.erase(remove_if(v.begin(), v.end(), rambda), v.end());


		if (tq.empty() != true)
		{
			int truck = tq.front();
			if (truck + w <= weight)
				// 트럭이 올라갈 수 있다면
			{
				pair<int, int> pair(truck, bridge_length);
				v.push_back(pair);
				tq.pop();
				// 트럭을 다리위로 올린다.
				w += truck;
			}
		}


		if (tq.empty() && v.empty())
			break;
	}

	//cout << answer;

	return answer;
}

int main()
{
	//solution(2, 10, { 7,4,5,6 });
	//solution(100, 100, { 10 });
	solution(100, 100, { 10,10,10,10,10,10,10,10,10,10 });


	return 0;
}
#endif // 다리를_지나는_트럭
#ifdef 가장_큰_수
#include <iostream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

bool cmp(const int& a, const int& b)
{
	string str1 = to_string(a);
	string str2 = to_string(b);

	//cout << str1 << " " << str2 << endl;
	//cout << str1.size() << " " << str2.size() << endl;

	//if (str1.size() > str2.size())
	//{
	//    int size = str1.size() - str2.size();
	//    for (int i = 0; i < size; i++)
	//        str2.push_back('9');
	//}
	//else if(str2.size() > str1.size())
	//{
	//    int size = str2.size() - str1.size();
	//    for (int i = 0; i < size; i++)
	//        str1.push_back('9');
	//}

	//cout << str1 << " " << str2 << endl;
	//cout << "-----------------------------------" << endl;
	//cout << stoi(str1) << " " << stoi(str2) << endl;
	//cout <<(stoi(str1) >= stoi(str2)) << endl;
	string str3 = str1 + str2;
	string str4 = str2 + str1;

	return stoi(str3) > stoi(str4);
}

string solution(vector<int> numbers)
{
	string answer = "";

	sort(numbers.begin(), numbers.end(), cmp);

	for (auto i : numbers)
		answer += to_string(i);

	for (int i = 0; i < answer.size(); i++)
	{
		if (i == answer.size() - 1 && answer[i] == '0')
		{
			answer = "0";
			break;
		}

		if (answer[i] != '0')
		{
			break;
		}
	}
	return answer;
}

int main()
{
	cout << solution({ 6, 10, 2 }) << endl; // 6210
	cout << solution({ 3, 30, 34, 5, 9 }) << endl; // 9534330
	cout << solution({ 0,0,0,0 }) << endl; // 0 
	cout << solution({ 1, 10, 100, 1000, 818, 81, 898, 89, 0, 0 }) << endl; // 8989881881110100100000
	cout << solution({ 979, 97, 978, 81,818,817 }) << endl; // 9799797881881817 

	return 0;
}
#endif // 가장_큰_수
#ifdef 소수_찾기
#include <iostream>
#include <string>
#include <vector>
#include <set>

using namespace std;
set<int> sPrime;
bool isPrime(int n)
{
	if (n == 1 || n == 0)
		return false;

	for (int i = 2; i * i <= n; i++)
	{
		if (n % i == 0)
		{
			return false;
		}
	}

	return true;
}

void DFS(string num, string str)
{
	int pNum = stoi(str);
	if (isPrime(pNum))
		sPrime.insert(pNum);


	for (int i = 0; i < num.size(); i++)
	{
		string rStr = num;
		char c = num[i];

		rStr.erase(find(rStr.begin(), rStr.end(), num[i]));
		DFS(rStr, str + c);
	}

}

int solution(string numbers)
{
	DFS(numbers, "0");

	for (auto i : sPrime)
		cout << i << " ";
	cout << endl;

	return sPrime.size();
}

int main()
{
	cout << solution("17") << endl;
	//cout << solution("011") << endl;
}
#endif // 소수_찾기
#ifdef 튜플
#include <string>
#include <vector>
#include <iostream>
#include <algorithm>

using namespace std;

vector<int> solution(string s);

int main()
{
	string str = "{{2},{2,1},{2,1,3},{2,1,3,4}}";
	auto s = solution(str);
	for (int i = 0; i < s.size(); i++)
	{
		cout << to_string(s[i]) << " ";
	}

	return 0;
}

vector<int> solution(string s) {
	s.erase(s.end() - 1);
	s.erase(s.begin());

	vector<vector<int>> tuple;
	int index = -1;
	string value;
	bool isTuple = false;

	for (int i = 0; i < s.size(); i++)
	{
		if (s[i] == '{')
		{
			tuple.push_back(vector<int>());
			++index;
			continue;
		}
		else if (s[i] == ',')
		{
			if (value != "")
			{
				tuple[index].push_back(stoi(value));
				value = "";
			}
			continue;
		}
		else if (s[i] == '}')
		{
			if (value != "")
			{
				tuple[index].push_back(stoi(value));
				value = "";
			}
			continue;
		}
		else
		{
			value += s[i];
			continue;
		}
	}

	for (int i = 0; i < tuple.size(); i++)
	{
		sort(tuple[i].begin(), tuple[i].end());
	}
	sort(tuple.begin(), tuple.end(), [](vector<int> a, vector<int> b) {return a.size() < b.size(); });

	for (int i = 0; i < tuple.size() - 1; i++)
	{
		int index = *tuple[i].begin();
		for (int j = i + 1; j < tuple.size(); j++)
		{
			tuple[j].erase(find(tuple[j].begin(), tuple[j].end(), index));
		}
	}

	vector<int> answer;

	for (int i = 0; i < tuple.size(); i++)
	{
		answer.push_back(tuple[i][0]);
	}


	return answer;
}
#endif // 튜플
#ifdef 뒤에_있는_큰_수_찾기
#include <string>
#include <vector>
#include <iostream>
#include <stack>

using namespace std;
vector<int> solution(vector<int> numbers);

int main()
{
	auto vec = solution({ 9, 1, 5, 3, 6, 2 });
	for (int i = 0; i < vec.size(); i++)
	{
		cout << vec[i] << " ";
	}
}


vector<int> solution(vector<int> numbers)
{
	vector<int> answer(numbers.size());
	stack<int> s;
	s.push(0);

	for (int i = 1; i < numbers.size(); i++)
	{
		int top = numbers[s.top()];
		int number = numbers[i];

		if (top >= number)
		{
			s.push(i);
		}
		else
		{
			while (s.size() > 0 && numbers[s.top()] < number)
			{
				int topIndex = s.top();
				int numberIndex = numbers[i];
				answer[topIndex] = numberIndex;
				s.pop();
			}
			s.push(i);
		}
	}

	while (s.empty() != true)
	{
		answer[s.top()] = -1;
		s.pop();
	}

	return answer;
}

#endif // 뒤에_있는_큰_수_찾기
#ifdef 쿼드압축_후_개수_세기
#include <string>
#include <vector>
#include<iostream>

using namespace std;

vector<int> solution(vector<vector<int>> arr);
void recursion(vector<vector<int>> arr, vector<int>& answer);

int main()
{
	auto sol = solution({ {1,1,0,0},{1,0,0,0},{1,0,0,1},{1,1,1,1} });
	cout << sol[0] << " : " << sol[1] << endl;

	return 0;
}

vector<int> solution(vector<vector<int>> arr)
{
	vector<int> answer(2);
	recursion(arr, answer);
	return answer;
}

void recursion(vector<vector<int>> arr, vector<int>& answer)
{
	int first = arr[0][0];
	bool isSame = true;
	if (arr.size() == 1)
	{
		answer[first]++;
		return;
	}

	for (int i = 0; i < arr.size(); i++)
	{
		for (int j = 0; j < arr[i].size(); j++)
		{
			if (first != arr[i][j])
			{
				isSame = false;
				break;
			}
		}

		if (isSame == false)
			break;
	}

	if (isSame)
	{
		answer[first]++;
	}
	else
	{
		int center = arr.size() / 2;
		int end = arr.size();

		{
			// 좌 상단
			vector<vector<int>> quad;
			for (int i = 0; i < center; i++)
			{
				quad.push_back(vector<int>(arr[i].begin(), arr[i].begin() + center));
			}
			recursion(quad, answer);
		}

		{
			// 우 상단
			vector<vector<int>> quad;
			for (int i = 0; i < center; i++)
			{
				quad.push_back(vector<int>(arr[i].begin() + center, arr[i].end()));
			}
			recursion(quad, answer);
		}

		{
			// 좌 하단
			vector<vector<int>> quad;
			for (int i = center; i < end; i++)
			{
				quad.push_back(vector<int>(arr[i].begin(), arr[i].begin() + center));
			}
			recursion(quad, answer);
		}

		{
			// 우 하단
			vector<vector<int>> quad;
			for (int i = center; i < end; i++)
			{
				quad.push_back(vector<int>(arr[i].begin() + center, arr[i].end()));
			}
			recursion(quad, answer);
		}

	}
}

#endif // 쿼드압축_후_개수_세기
