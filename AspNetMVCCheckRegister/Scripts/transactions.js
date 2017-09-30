$(document).ready(function () {

  var balance = $('#transactionsTable tr td').last().html();
  var footer = $("#transactionsSummary");

  footer.html('Your current Balance is <strong>' + balance + '</strong>');
  if (balance > 0) {
    footer.css('color', 'green')
  }
  else {
    footer.css('color', 'red')
  }

});