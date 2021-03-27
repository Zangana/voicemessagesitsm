import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:itsmvoicemessages/audioplayer.dart';
import 'dart:async';
import 'dart:convert';
import 'package:itsmvoicemessages/connectionpath.dart';
import 'package:assets_audio_player/assets_audio_player.dart';
import './string_duration.dart';

class ViewTickets extends StatefulWidget {
  @override
  _ViewTickets createState() => _ViewTickets();
}

class _ViewTickets extends State<ViewTickets> {
  List data;
  String _incidentTicketValue = null;
  String _incidentLoggingValue = null;
  String _incidentPrioritiesValue = null;
  String _incidentCategoriesValue = null;

  List<String> _incidentTicketValues = new List<String>();
  List<String> _incidentLoggingValues = new List<String>();
  List<String> _incidentPrioritiesValues = new List<String>();
  List<String> _incidentCategoriesValues = new List<String>();
  TextEditingController editingController = TextEditingController();

  Future getData() async {
    Map<String, String> _headers = {
      'Content-Type': 'application/json;charse=UTF-8',
      'charset': 'UTF-8'
    };

    final response =
        await http.get(getPath() + 'IncidentManagements', headers: _headers);
    if (response.statusCode == 200) {
      data = json.decode(response.body);

      return data;
    } else {
      throw Exception('Failed to load data' + response.statusCode.toString());
    }
  }

  @override
  void initState() {
    _incidentTicketValues.addAll(["Incidnet", "Service Request"]);
    _incidentLoggingValues
        .addAll(["Phone Calls", "Email", "SMS", "Used the app"]);
    _incidentCategoriesValues.addAll(["Critical", "High", "Medium", "Low"]);
    _incidentPrioritiesValues.addAll(["High", "Medium", "Low"]);
    getData();
    setState(() {});
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.green,
        centerTitle: true,
        title: Text("List of Tickets",
            style: TextStyle(fontWeight: FontWeight.bold, fontSize: 25)),
      ),
      body: FutureBuilder<dynamic>(
        future: getData(),
        builder: (context, snapshot) {
          if (snapshot.hasData) {
            return Container(
              child: Column(
                children: [
                  Padding(
                      padding: const EdgeInsets.all(8.0),
                      child: TextField(
                        onChanged: (value) {},
                        controller: editingController,
                        decoration: InputDecoration(
                            labelText: "Please enter user's name",
                            prefixIcon: Icon(Icons.search),
                            border: OutlineInputBorder(
                                borderRadius:
                                    BorderRadius.all(Radius.circular(25.0)))),
                      )),
                  Expanded(
                      child: ListView.builder(
                    shrinkWrap: true,
                    itemCount: data.length,
                    itemBuilder: (context, index) {
                      return Card(
                          child: Container(
                              decoration: BoxDecoration(
                                border: Border(
                                  top: BorderSide(
                                    width: 2.0,
                                    color: Colors.black,
                                  ),
                                ),
                                color: Colors.black87,
                              ),
                              child: Padding(
                                  padding: const EdgeInsets.all(16.0),
                                  child: ExpansionTile(
                                    title: Text(
                                      "${index + 1}" +
                                          " " +
                                          _incidentTicketValues
                                              .elementAt(checkFornull(
                                                      "${data[index]['incidentTicket']}") -
                                                  1)
                                              .toString() +
                                          " | " +
                                          checkForClosure(
                                              "${data[index]['incidentClosures']}"), //add Vlaue
                                      style: TextStyle(
                                        fontSize: 22.0,
                                        color: Colors.white,
                                      ),
                                    ),
                                    children: [
                                      RichText(
                                        textDirection: TextDirection.ltr,
                                        textAlign: TextAlign.left,
                                        text: TextSpan(
                                            style: TextStyle(
                                              fontSize: 20.0,
                                              color: Colors.white,
                                            ),
                                            children: [
                                              TextSpan(
                                                  text: "Date: ${data[index]['incidentDate']}" +
                                                      '\n' +
                                                      "Incident Ticket: " +
                                                      _incidentTicketValues
                                                          .elementAt(checkFornull(
                                                                  "${data[index]['incidentTicket']}") -
                                                              1)
                                                          .toString() +
                                                      '\n' +
                                                      "Incident Logging: " +
                                                      _incidentLoggingValues
                                                          .elementAt(checkFornull(
                                                                  "${data[index]['incidentLoggings']}") -
                                                              1)
                                                          .toString() +
                                                      '\n' +
                                                      "Incident Closure: " +
                                                      checkForClosure(
                                                          "${data[index]['incidentClosures']}") +
                                                      '\n' +
                                                      "Incident Priorities: " +
                                                      _incidentPrioritiesValues
                                                          .elementAt(checkFornull(
                                                                  "${data[index]['incidentPriorities']}") -
                                                              1)
                                                          .toString() +
                                                      '\n' +
                                                      "Incident Categories: " +
                                                      _incidentCategoriesValues
                                                          .elementAt(checkFornull(
                                                                  "${data[index]['incidentpriorities']}") -
                                                              1)
                                                          .toString() +
                                                      '\n' +
                                                      printDescriptioin(
                                                          data, index)),
                                            ]),
                                      ),
                                      Container(
                                        decoration: BoxDecoration(
                                          border: Border(
                                            top: BorderSide(
                                              width: 2.0,
                                              color: Colors.black,
                                            ),
                                          ),
                                          color: Colors.black87,
                                        ),
                                        padding: const EdgeInsets.all(16.0),
                                        width:
                                            MediaQuery.of(context).size.width *
                                                0.30,
                                        height:
                                            MediaQuery.of(context).size.height *
                                                0.30,
                                        child: audioplayer(),
                                      )
                                    ],
                                  ))));
                    },
                  )),
                ],
              ),
            );
          } else //(snapshot.hasError)
          {
            return Text("${snapshot.error}");
          } //else {
          //return CircularProgressIndicator();
          // }
        },
      ),
    );
  }

  int checkFornull(String num) {
    return int.tryParse(num) ?? 1;
  }

  String checkForClosure(String num) {
    int val = int.tryParse(num) ?? -1;
    if (val > -1) {
      return "Closed";
    } else {
      return "Open";
    }
  }
}

String checkForAvailabilityOfAudio(dynamic items, index) {
  try {
    return "https://localhost:5001/uploads/${items[index]['voicemessages'][0]['voiceName']}";
  } catch (e) {
    return 'none';
  }
}

String printDescriptioin(dynamic items, int index) {
  try {
    return "Descriptions: " +
        "${items[index]['descriptions'][0]['describe']} \n" +
        "Respond: " +
        "${items[index]['descriptions'][0]['operatorsRespond']}";
  } catch (e) {
    return 'Descriptions: Empty';
  }
}
