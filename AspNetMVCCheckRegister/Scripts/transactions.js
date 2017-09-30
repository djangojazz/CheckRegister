 $(document).ready(function () {
      var balance = $('#transactionsTable tr td').last().html();
      var footer = $("#transactionsSummary");

      footer.html('Your current Balance is <strong>' + balance + '</strong>');
      if (balance > 0) {
        footer.css('color', '#00ff99')
      }
      else {
        footer.css('color', '#ff6666')
      }

});