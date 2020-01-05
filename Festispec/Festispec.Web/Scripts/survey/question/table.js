class TableQuestion {

	constructor(tableId, inputName) {
        this.scope = document.querySelector("#" + tableId);
        this.table = this.scope.querySelector("table");
		this.inputField = document.querySelector("input[name='" + inputName + "']");
		this.addListners();
    }

    getValues() {
        let tableAnswerRows = this.table.querySelectorAll("tbody tr");
        let rowAnswers = [];
        tableAnswerRows.forEach((row) => {
            let tds = row.querySelectorAll("td");
            let rowValues = [];
            tds.forEach((tdElement) => {
                let inputElement = tdElement.firstElementChild;
                if (inputElement.hasAttribute("type") && inputElement.className != "delete-row") {
                    // Is it a text field
                    let value = inputElement.value;
                    rowValues.push(value);
                }
                else if (inputElement.tagName == "SELECT"){
                    // It is a select
                    let selectedValue = inputElement.querySelector("option:checked");
                    if (selectedValue == null) {
                        rowValues.push("");
                    }
                    else {
                        rowValues.push(selectedValue.value);
                    }
                }
            });
            rowAnswers.push(rowValues);
        });
        return rowAnswers;
    }

	addListners() {
        this.table.addEventListener('change', (event) => {
            this.setNewValues(JSON.stringify(this.getValues()));
        });
        this.table.querySelector(".delete-row").addEventListener('click', (event) => {
            if (this.table.querySelectorAll("tbody tr").length > 1) {
                let row = event.currentTarget.parentNode.parentNode;
                console.log(row);
                row.remove();
            }
        });
        this.scope.querySelector(".add-row").addEventListener('click', () => {
            let row = this.table.querySelector("tbody tr");
            let clonedItem = row.cloneNode(true);
            let tds = clonedItem.querySelectorAll("td");
            tds.forEach((td) => {
                let input = td.querySelector("input");
                if (input) {
                    input.value = "";
                }
            });
            clonedItem.querySelector(".delete-row").addEventListener('click', (event) => {
                if (this.table.querySelectorAll("tbody tr").length > 1) {
                    let row = event.currentTarget.parentNode.parentNode;
                    row.remove();
                }
            });
            this.table.querySelector("tbody").appendChild(clonedItem);
        });
        let event = new Event('change');
        this.table.dispatchEvent(event);
    }

    displayNotCompleted() {
        this.scope.querySelector(".table-error").innerHTML = "Niet goed ingevuld";
    }

	setNewValues(value) {
		this.inputField.value = value;
    }

    clear() {
        this.scope.querySelector(".table-error").innerHTML = "";
    }

    loadAnswer() {
        let event = new Event('change');
        this.table.dispatchEvent(event);
    }

    isValid() {
        let tableQuestionIsValid = true;
        this.loadAnswer();
        let tableQuestionValues = this.getValues();
        for (let i = 0; i < tableQuestionValues.length; i++) {
            let tableQuestionRow = tableQuestionValues[i];
            let foundCorrectValues = 0;
            for (let j = 0; j < tableQuestionRow.length; j++) {
                let tableColValue = tableQuestionRow[j];
                if (tableColValue && tableColValue != "") {
                    foundCorrectValues++;
                }
            }
            if (foundCorrectValues != tableQuestionRow.length) {
                tableQuestionIsValid = false;
            }
        }
        return tableQuestionIsValid;
    }
}