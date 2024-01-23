using System;

namespace MortgageCalculator
{
    class Program
    {
        static void Main()
        {
            int invalidEntryCount = 0;

            // Collect input from the loan officer
            decimal purchasePrice = GetDecimalInput("Enter home purchase price: ");
            decimal downPayment = GetDecimalInput("Enter down payment amount: ");
            decimal interestRate = GetDecimalInput("Enter annual interest rate: ");
            decimal monthlyIncome = GetDecimalInput("Enter monthly income: ");

            int loanTerm;
            do
            {
                loanTerm = GetIntInput("Enter loan term (15 or 30 years): ");

                // Validate loan term
                if (loanTerm != 15 && loanTerm != 30)
                {
                    Console.WriteLine("Invalid loan term. Please enter 15 or 30.");
                    invalidEntryCount++;

                    if (invalidEntryCount >= 3)
                    {
                        Console.WriteLine("Too many invalid entries. Exiting program.");
                        return;
                    }
                }

            } while (loanTerm != 15 && loanTerm != 30);

            // Reset invalid entry count
            invalidEntryCount = 0;

            // Calculate total loan value
            decimal totalLoanValue = CalculateTotalLoanValue(purchasePrice, downPayment);

            // Calculate equity percentage and value
            decimal equityPercentage = CalculateEquityPercentage(purchasePrice, totalLoanValue, downPayment);
            decimal equityValue = CalculateEquityValue(purchasePrice, totalLoanValue, downPayment);

            // Calculate monthly loan payment using the formula
            decimal monthlyPayment = CalculateMonthlyPayment(totalLoanValue, interestRate, loanTerm);

            // Display total loan value, equity, and monthly payment
            Console.WriteLine($"Total Loan Value: {totalLoanValue:C}");
            Console.WriteLine($"Equity Percentage: {equityPercentage:P}");
            Console.WriteLine($"Equity Value: {equityValue:C}");
            Console.WriteLine($"Monthly Payment: {monthlyPayment:C}");

            // Check if loan insurance is required
            if ((double)equityPercentage < 0.10)
            {
                decimal loanInsurance = CalculateLoanInsurance(totalLoanValue);
                monthlyPayment += loanInsurance;
                Console.WriteLine($"Loan Insurance: {loanInsurance:C}");
            }

            // Additional calculations and recommendations
            // ...

            // Check if the payment is >= 25% of the buyer's monthly income
            if (monthlyPayment >= 0.25M * monthlyIncome)
            {
                Console.WriteLine("Loan denied. Consider placing more money down or looking at a more affordable home.");
            }
            else
            {
                Console.WriteLine("Loan approved!");
            }
        }

        // Helper method to get decimal input
        static decimal GetDecimalInput(string prompt)
        {
            Console.Write(prompt);
            return Convert.ToDecimal(Console.ReadLine());
        }

        // Helper method to get integer input
        static int GetIntInput(string prompt)
        {
            Console.Write(prompt);
            return Convert.ToInt32(Console.ReadLine());
        }

        // Helper method to calculate total loan value
        static decimal CalculateTotalLoanValue(decimal purchasePrice, decimal downPayment)
        {
            decimal originationFee = 0.01M * (purchasePrice - downPayment);
            decimal taxesAndClosingCosts = 2500;
            decimal propertyTaxPercentage = 0.0125M;

            // Calculate property tax for the year
            decimal propertyTax = propertyTaxPercentage * purchasePrice;

            return purchasePrice + originationFee + taxesAndClosingCosts + propertyTax;
        }

        // Helper method to calculate equity percentage
        static decimal CalculateEquityPercentage(decimal purchasePrice, decimal totalLoanValue, decimal downPayment)
        {
            return (purchasePrice - downPayment) / totalLoanValue;
        }

        // Helper method to calculate equity value
        static decimal CalculateEquityValue(decimal purchasePrice, decimal totalLoanValue, decimal downPayment)
        {
            return purchasePrice - totalLoanValue + downPayment;
        }

        // Helper method to calculate monthly loan payment
        static decimal CalculateMonthlyPayment(decimal totalLoanValue, decimal interestRate, int loanTerm)
        {
            decimal annualInterestRate = interestRate / 100;
            decimal monthlyInterestRate = annualInterestRate / 12;
            int numberOfPayments = loanTerm * 12;

            decimal power = (decimal)Math.Pow(1 + (double)monthlyInterestRate, -numberOfPayments);
            return (totalLoanValue * monthlyInterestRate) / (1 - power);
        }

        // Helper method to calculate loan insurance
        static decimal CalculateLoanInsurance(decimal initialLoanValue)
        {
            decimal insuranceRate = 0.01M;
            return initialLoanValue * insuranceRate; // Added closing parenthesis and semicolon
                }
            }
        }
