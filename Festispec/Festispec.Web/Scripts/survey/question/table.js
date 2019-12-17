class TableQuestion {

	constructor(tableId, inputName) {

		this.scope = document.querySelector("#"+ tableId);
		this.inputField = document.querySelector("input[name='" + inputName + "']");
		this.addListners();
		
	}

	addListners() {
		this.scope.addEventListener('change', (event) => {
			console.log("doei");
			let tableAnswerRows = this.scope.querySelectorAll("tbody tr");
			let rowAnswers = [];
			tableAnswerRows.forEach((row) => {
				console.log(row);
			});
		});
	}

	setNewValues(value) {
		this.inputField.value = value;
	}
}