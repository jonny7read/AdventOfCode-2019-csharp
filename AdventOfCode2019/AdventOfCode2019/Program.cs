using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScratchPad
{
    class Program
    {
        static void Main(string[] args)
        {
            new Day2().Run();
        }

        class Day2 : Task
        {
            protected override int Day => 2;

            protected override string GetAnswerPart1(string[] input)
            {
                var answer = string.Empty;
                var nums = GetProgramState(input, 12, 2);
                var output = RunInstructions(nums);

                answer = output.ToString();

                return answer;
            }

            private static int RunInstructions(List<int> nums)
            {
                for (var i = 0; i < nums.Count / 4; i++)
                {
                    var baseIndex = i * 4;
                    var opcode = nums[baseIndex];
                    if (opcode == 99)
                    {
                        //Console.WriteLine("99 opcode, exiting");
                        break;
                    }
                    else if (opcode == 1 || opcode == 2)
                    {
                        var pos1 = nums[baseIndex + 1];
                        var pos2 = nums[baseIndex + 2];
                        var resultPos = nums[baseIndex + 3];
                        nums[resultPos] = opcode == 1
                            ? nums[pos1] + nums[pos2]
                            : nums[pos1] * nums[pos2];
                    }
                    else
                    {
                        Console.WriteLine($"Unrecognised opcode: {opcode}, exiting");
                        break;
                    }
                }

                return nums[0];
            }

            private static List<int> GetProgramState(string[] input, int noun, int verb)
            {
                var nums = input[0].Split(',').Select(int.Parse).ToList();

                // set "1202 program alarm" state
                nums[1] = noun;
                nums[2] = verb;
                return nums;
            }

            protected override string GetAnswerPart2(string[] input)
            {
                var answer = string.Empty;
                const int wanted = 19690720;
                for (var noun = 0; noun < 100; noun++)
                {
                    for (var verb = 0; verb < 100; verb++)
                    {
                        var nums = GetProgramState(input, noun, verb);
                        var output = RunInstructions(nums);
                        if (output == wanted)
                        {
                            var winner = 100 * noun + verb;
                            answer = winner.ToString();
                            break;
                        }
                    }
                }

                return answer;
            }
        }

        class Day1 : Task
        {
            protected override int Day => 1;

            protected override string GetAnswerPart1(string[] input)
            {
                var nums = input.Select(s =>
                {
                    var num = double.Parse(s);
                    var fuel = Math.Floor(num / 3) - 2;
                    return fuel;
                });

                return nums.Sum().ToString();
            }

            protected override string GetAnswerPart2(string[] input)
            {
                var nums = input.Select(s =>
                {
                    var num = double.Parse(s);
                    var fuel = Math.Floor(num / 3) - 2;
                    var totalFuel = 0d;
                    while (fuel > 0)
                    {
                        totalFuel += fuel;
                        fuel = Math.Floor(fuel / 3) - 2;
                    }
                    return totalFuel;
                });

                return nums.Sum().ToString();
            }
        }

        abstract class Task
        {
            protected abstract int Day { get; }

            private string[] GetInput()
            {
                return File.ReadAllLines($"Day{Day}Input.txt");
            }

            protected abstract string GetAnswerPart1(string[] input);
            protected abstract string GetAnswerPart2(string[] input);

            public void Run()
            {
                var input = GetInput();
                Console.WriteLine($"Part 1: " + GetAnswerPart1(input));
                Console.WriteLine($"Part 2: " + GetAnswerPart2(input));
            }
        }

    }
}
