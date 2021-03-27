import 'dart:convert';
import 'dart:io';
import 'package:http/http.dart' as http;
import 'package:itsmvoicemessages/connectionpath.dart';

Map<String, String> _headers = {
  'Content-Type': 'application/json;charse=UTF-8',
  'charset': 'UTF-8'
};
String _url = getPath();

Future callIncidentTicket(int ticket, int logging, int priority, int category,
    String description) async {
  var params = {
    "IncidentTicket": ticket,
    "IncidentLoggings": logging,
    "IncidentPriorities": priority,
    "IncidentCategories": category,
    "descriptions": [
      {"describe": description, "desEnteredby": null}
    ]
  };
  final jsonEncode = json.encode(params);
  final response = await http.post(_url + "IncidentManagements",
      body: jsonEncode, headers: _headers);
  stderr.writeln(response.statusCode);
  if (response.statusCode == 200) {
    stderr.writeln(response.statusCode);

    return 200;
  } else {
    stderr.writeln(response.statusCode);
  }
}
