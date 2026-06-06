const apiBase = "/api";

async function runPayroll() {

    const button =
        document.getElementById("runPayrollBtn");

    button.disabled = true;
    button.innerText = "Running...";

    try {

        const month =
            document.getElementById("runMonth").value;

        const year =
            document.getElementById("runYear").value;

        const response =
            await fetch(`${apiBase}/payroll/run`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    month,
                    year
                })
            });

        const result = await response.json();

        const resultDiv =
            document.getElementById("runResult");

        if (response.ok) {

            resultDiv.innerHTML =
                `<div class="success">
                    Payroll run created successfully.
                    Run Id: ${result.payrollRunId}
                </div>`;
        }
        else {

            resultDiv.innerHTML =
                `<div class="error">
                    ${result.message}
                </div>`;
        }
    }
    finally {

        button.disabled = false;
        button.innerText = "Run Payroll";
    }
}

async function loadPayroll() {

    const button =
        document.getElementById("loadPayrollBtn");

    button.disabled = true;
    button.innerText = "Loading...";

    try {

        const month =
            document.getElementById("searchMonth").value;

        const year =
            document.getElementById("searchYear").value;

        const response =
            await fetch(
                `${apiBase}/payroll/${month}/${year}`);

        const data = await response.json();

        const tbody =
            document.querySelector("#payrollTable tbody");

        tbody.innerHTML = "";

        if (data.length === 0) {

            tbody.innerHTML = `
        <tr>
            <td colspan="6"
                style="text-align:center;">
                No payroll records found.
            </td>
        </tr>
    `;

            return;
        }

        tbody.innerHTML = "";

        data.forEach(item => {

            tbody.innerHTML += `
            <tr>
                <td>${item.name}</td>
                <td>₹${Number(item.grossPay).toLocaleString()}</td>
                <td>₹${Number(item.pfDeduction).toLocaleString()}</td>
                <td>₹${Number(item.professionalTax).toLocaleString()}</td>
                <td>₹${Number(item.netPay).toLocaleString()}</td>
                <td>
                    <button
                        onclick="showPayslip(
                           ${item.employeeId},
                          ${item.payrollRunId})">
                          Payslip
                    </button>
                </td>
            </tr>
        `;
        });
    }
    finally {
        button.disabled = false;
        button.innerText = "Load Payroll";
    }
}

async function showPayslip(
    employeeId,
    payrollRunId) {

    const response =
        await fetch(
            `${apiBase}/payroll/${payrollRunId}/slip/${employeeId}`);

    const data =
        await response.json();

    document.getElementById(
        "payslipContent").innerHTML = `

        <div class="payslip-row">
            <strong>Employee</strong>
            <span>${data.employeeName}</span>
        </div>

        <div class="payslip-row">
            <strong>Basic Salary</strong>
            <span>₹${Number(data.basicSalary).toLocaleString()}</span>
        </div>

        <div class="payslip-row">
            <strong>Days Present</strong>
            <span>${data.daysPresent}</span>
        </div>

        <div class="payslip-row">
            <strong>Gross Pay</strong>
            <span>₹${Number(data.grossPay).toLocaleString()}</span>
        </div>

        <div class="payslip-row">
            <strong>PF Deduction</strong>
            <span>₹${Number(data.pfDeduction).toLocaleString()}</span>
        </div>

        <div class="payslip-row">
            <strong>Professional Tax</strong>
            <span>₹${Number(data.professionalTax).toLocaleString()}</span>
        </div>

        <div class="payslip-row">
            <strong>Net Pay</strong>
            <span>₹${Number(data.netPay).toLocaleString()}</span>
        </div>
    `;

    document.getElementById(
        "payslipModal").style.display =
        "block";
}

function closePayslip() {

    document.getElementById(
        "payslipModal").style.display =
        "none";
}

function printPayslip() {

    const content =
        document.getElementById(
            "payslipContent").innerHTML;

    const printWindow =
        window.open('', '', 'width=800,height=600');

    printWindow.document.write(`
        <html>
        <head>
            <title>Payslip</title>
        </head>
        <body>
            <h2>Payslip</h2>
            ${content}
        </body>
        </html>
    `);

    printWindow.document.close();

    printWindow.print();
}

window.onload = () => {

    const today = new Date();

    document.getElementById("runMonth").value =
        today.getMonth() + 1;

    document.getElementById("runYear").value =
        today.getFullYear();

    document.getElementById("searchMonth").value =
        today.getMonth() + 1;

    document.getElementById("searchYear").value =
        today.getFullYear();
};