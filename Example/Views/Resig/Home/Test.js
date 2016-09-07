function lorem(model) {
  return 'lorem ipsum ' + model.Foo + ' ' + model.Bar;
}

function bind(model, bag) {
  var loremIpsum = lorem(model);
  $('.foo').val(model.Foo);
  $('#bar').val(model.Bar);
  $('.bar').val(model.Bar);
  $('.baz').text(model.Baz);
  $('#baz').html(loremIpsum);
  log('bag: ' + bag);
  log('bag.Bacon: ' + bag.Bacon);
  $('.bacon').text(bag.Bacon + ' lazy dogs.');
}
