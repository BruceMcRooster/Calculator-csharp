bool more = true;
string prev_result = null;

while (more)
{
    Console.WriteLine("Enter your equation");
    string input = solve(check_combine(Console.ReadLine().Replace(" ", ""),prev_result));
    prev_result = input;
    Console.WriteLine(input);
    Console.Write("----------\nDo you have another equation? (enter anything to quit)");
    if (Console.ReadLine().Length > 0) { more = false; }
    else { Console.WriteLine("-----------"); }
}

string check_combine(string input, string prev_input)
{
    if (input[0] == '*' || input[0] == '/' || input[0] == '+' || input[0] == '-' || input[0] == '-' || input[0] == '(')
    {
        return prev_input + input;
    }
    else
    {
        return input;
    }
}

string solve(string equ)
{
    return add_subtract(multiply_divide(exponents(parentheses(parentheses_multiplication_edit(equ)))));
}

string parentheses_multiplication_edit(string equ)
{
    for (int i = 1; i < equ.Length - 1; i++)
    {
        if (equ[i] == '(' && (!(equ[i - 1] == '+') && !(equ[i - 1] == '-') && !(equ[i - 1] == '*') && !(equ[i - 1] == '/') && !(equ[i - 1] == '^')))
        {
            equ = insert(equ, i, i, "*(");
        }
        else if (equ[i] == ')' && (!(equ[i + 1] == '+') && !(equ[i + 1] == '-') && !(equ[i + 1] == '*') && !(equ[i + 1] == '/') && !(equ[i + 1] == '^')))
        {
            equ = insert(equ, i, i, ")*");
        }
    }
    return equ;
}

string parentheses(string input)
{
    while (input.IndexOf('(') > -1)
    {

        int open_parentheses = 0;
        for (int i = input.IndexOf('(') + 1; i < input.Length; i++)
        {
            if (input[i] == '(')
            {
                open_parentheses++;
                continue;
            }

            if (input[i] == ')' && open_parentheses > 0) { open_parentheses--; }
            else if (input[i] == ')')
            {
                input = insert(input, input.IndexOf('('), i, solve(java_substring(input, input.IndexOf('(') + 1, i)));
                break;
            }
        }


    }
    return input;
}

string exponents(string input)
{
    while (input.IndexOf('^') > -1)
    {
        int index = input.IndexOf('^');

        int pt1 = find_pt1(input, index);
        int pt2 = find_pt2(input, index);

        input = insert(input, index - Convert.ToString(pt1).Length, index + Convert.ToString(pt2).Length, Convert.ToString((int)Math.Pow(pt1, pt2)));
    }
    return input;
}
string multiply_divide(string input)
{
    while (input.IndexOf('*') > -1 || input.IndexOf('/') > -1)
    {
        int index;
        bool multiply;
        if (input.IndexOf('*') == -1) { index = input.IndexOf('/'); multiply = false; }
        else if (input.IndexOf('/') == -1) { index = input.IndexOf('*'); multiply = true; }
        else
        {
            if (input.IndexOf('*') < input.IndexOf('/'))
            {
                index = input.IndexOf('*');
                multiply = true;
            }
            else
            {
                index = input.IndexOf('/');
                multiply = false;
            }
        }
        int pt1 = find_pt1(input, index);
        int pt2 = find_pt2(input, index);

        input = insert(input, index - Convert.ToString(pt1).Length, index + Convert.ToString(pt2).Length, Convert.ToString((multiply) ? pt1 * pt2 : pt1 / pt2));
    }
    return input;
}
string add_subtract(string input)
{
    while (input.IndexOf('+') > -1 || input.IndexOf('-') > -1)
    {
        int index;
        bool add;
        if (input.IndexOf('+') == -1) { index = input.IndexOf('-'); add = false; }
        else if (input.IndexOf('-') == -1) { index = input.IndexOf('+'); add = true; }
        else
        {
            if (input.IndexOf('+') < input.IndexOf('-'))
            {
                index = input.IndexOf('+');
                add = true;
            }
            else
            {
                index = input.IndexOf('-');
                add = false;
            }
        }

        int pt1 = find_pt1(input, index);
        int pt2 = find_pt2(input, index);

        input = insert(input, index - Convert.ToString(pt1).Length, index + Convert.ToString(pt2).Length, Convert.ToString((add) ? pt1 + pt2 : pt1 - pt2));
    }
    return input;
}

int find_pt1(string equ, int on)
{
    int returned = 0;
    for (int i = on - 1; i >= -1; i--)
    {
        if (i == -1)
        {
            returned = Convert.ToInt32(java_substring(equ, 0, on));
        }
        else if (equ[i] == '(' || equ[i] == '+' || equ[i] == '-' || equ[i] == '*' || equ[i] == '/' || equ[i] == '^')
        {
            returned = Convert.ToInt32(java_substring(equ, i + 1, on));
            break;
        }
    }
    return returned;
}

int find_pt2(string equ, int on)
{
    int returned = 0;
    for (int i = on + 1; i <= equ.Length; i++)
    {
        if (i == equ.Length)
        {
            returned = Convert.ToInt32(equ.Substring(on + 1));
            break;
        }
        if (equ[i] == '(' || equ[i] == '+' || equ[i] == '-' || equ[i] == '*' || equ[i] == '/' || equ[i] == '^')
        {
            returned = Convert.ToInt32(equ.Substring(on + 1, i));
            break;
        }
    }
    return returned;
}

string insert(string equ, int start, int end, string what)
{
    return equ.Substring(0, start) + what + equ.Substring(end + 1);
}

string java_substring(string some_string, int start, int end)
{
    return some_string.Substring(start, end - start);
}