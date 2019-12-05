class SurveyConductor {

	// SCOPE - container
    constructor(identifer) {
		this.scope = document.querySelector(identifer);
		this.addListeners();
	}

	addListeners() {
		this.scope.querySelector("#saveSurvey").addEventListener('click', (e) => this.saveSurveyAnswersToLocalStorage());
	}

    getQuestion() {
        let questions = [];

	}

	getAnswers() {

	}

	isRequired(inputElement) {
		let containsAttribute = inputElement.containsAttribute("required");
		if (containsAttribute === true) {
			let attributeValue = inputElement.getAttribute("required");
			return attributeValue;
		}
		return false;
	}

	saveSurveyAnswersToLocalStorage() {
		
	}
}


(function () {
	let storage = window.localStorage;
	$("form").submit((event) => {
		event.preventDefault();
		if ($("form").valid()) {
			let formResult = $("form").serializeArray();
			
			saveToLocalStorage(formResult);
		}
		else {
			alert("survey not filled in correctly");
		}
	});

	function getCasesFromLocalStorage() {
		return storage.getItem("survey");
	}

	function saveToLocalStorage(result) {
		let surveyCases = getCasesFromLocalStorage();
		if (surveyCases == null) {
			surveyCases = {};
		}
		else {
			surveyCases = JSON.parse(surveyCases);
		}
		if (surveyCases.hasOwnProperty(surveyId)) {
			let cases = surveyCases[surveyId];
			console.log(cases);
		}
		else {
			surveyCases[surveyId] = [];
			let cases = surveyCases[surveyId];
		}
		localStorage.setItem("survey", JSON.stringify(surveyCases));
	}

})();


let surveyCon = new SurveyConductor(".survey-container");