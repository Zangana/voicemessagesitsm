import 'package:flutter/material.dart';
import 'callapi.dart';
import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';

class OpenTicket extends StatefulWidget {
  @override
  _OpenTicketState createState() => _OpenTicketState();
}

class _OpenTicketState extends State<OpenTicket> {
  TextEditingController descriptionTextController = new TextEditingController();
  ScrollController _scrollController;
  double _scrollPosition = 0;
  double _opacity = 0;
  String _incidentTicketValue = null;
  String _incidentLoggingValue = null;
  String _incidentPrioritiesValue = null;
  String _incidentCategoriesValue = null;

  List<String> _incidentTicketValues = new List<String>();
  List<String> _incidentLoggingValues = new List<String>();
  List<String> _incidentPrioritiesValues = new List<String>();
  List<String> _incidentCategoriesValues = new List<String>();
  void _onChangedTicketValue(String incidentTicketValue) {
    setState(() {
      _incidentTicketValue = incidentTicketValue;
    });
  }

  void _onChangedLogginValue(String incidentLoggingValue) {
    setState(() {
      _incidentLoggingValue = incidentLoggingValue;
    });
  }

  void _onChangedPrioritiesValue(String incidentPrioritiesValue) {
    setState(() {
      _incidentPrioritiesValue = incidentPrioritiesValue;
    });
  }

  void _onChangedCategoriesValue(String incidentCategoriesValue) {
    setState(() {
      _incidentCategoriesValue = incidentCategoriesValue;
    });
  }

  _scrollListener() {
    setState(() {
      _scrollPosition = _scrollController.position.pixels;
    });
  }

  @override
  void initState() {
    _scrollController = ScrollController();
    _scrollController.addListener(_scrollListener);
    _incidentTicketValues.addAll(["Incidnet", "Service Request"]);
    _incidentTicketValue = _incidentTicketValues.elementAt(0);
    _incidentLoggingValues
        .addAll(["Phone Calls", "Email", "SMS", "Used the app"]);
    _incidentLoggingValue = _incidentLoggingValues.elementAt(0);
    _incidentCategoriesValues.addAll(["Critical", "High", "Medium", "Low"]);
    _incidentCategoriesValue = _incidentCategoriesValues.elementAt(0);
    _incidentPrioritiesValues.addAll(["High", "Medium", "Low"]);
    _incidentPrioritiesValue = _incidentPrioritiesValues.elementAt(0);
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    var screenSize = MediaQuery.of(context).size;
    _opacity = _scrollPosition < screenSize.height * 0.40
        ? _scrollPosition / (screenSize.height * 0.40)
        : 1;

    return Scaffold(
        appBar: AppBar(
          backgroundColor: Colors.blue,
          elevation: 0,
          title: Text(
            "ITSM Voice Messages",
            style: TextStyle(
              color: Colors.white,
              fontSize: 20,
              fontWeight: FontWeight.w400,
              letterSpacing: 3,
            ),
          ),
        ),
        body: SingleChildScrollView(
          child: new Column(
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              new Container(
                //padding: new EdgeInsets.all(40.0),
                height: 350.0,
                child: new Column(
                  children: [
                    new DropdownButton(
                        value: _incidentTicketValue,
                        onChanged: (String value) {
                          _onChangedTicketValue(value);
                        },
                        items: _incidentTicketValues.map((String value) {
                          return new DropdownMenuItem(
                            value: value,
                            child: new Row(
                              children: [
                                new Icon(Icons.inbox),
                                new Text('Incidnet Ticket: ${value}')
                              ],
                            ),
                          );
                        }).toList()), //Incident Ticket dropdown button
                    new DropdownButton(
                        value: _incidentLoggingValue,
                        onChanged: (String value) {
                          _onChangedLogginValue(value);
                        },
                        items: _incidentLoggingValues.map((String value) {
                          return new DropdownMenuItem(
                            value: value,
                            child: new Row(
                              children: [
                                new Icon(Icons.device_hub),
                                new Text('Incident Logging: ${value}')
                              ],
                            ),
                          );
                        }).toList()), // Incident Logging Dropbutton
                    new DropdownButton(
                      value: _incidentPrioritiesValue,
                      onChanged: (String value) {
                        _onChangedPrioritiesValue(value);
                      },
                      items: _incidentPrioritiesValues.map((String value) {
                        return new DropdownMenuItem(
                          value: value,
                          child: new Row(
                            children: [
                              new Icon(Icons.priority_high_outlined),
                              new Text('Incident Priority: ${value}')
                            ],
                          ),
                        );
                      }).toList(),
                    ), //Incident Priority DropDown button
                    new DropdownButton(
                        value: _incidentCategoriesValue,
                        onChanged: (String value) {
                          _onChangedCategoriesValue(value);
                        },
                        items: _incidentCategoriesValues.map((String value) {
                          return new DropdownMenuItem(
                            value: value,
                            child: new Row(
                              children: [
                                new Icon(Icons.category),
                                new Text('Incident Category: ${value}')
                              ],
                            ),
                          );
                        }).toList()), //Incident Categories Drop Down Button
                    Expanded(
                      ///Descriptions text field.
                      child: new TextField(
                        controller: descriptionTextController,
                        decoration: new InputDecoration(
                            border: new OutlineInputBorder(
                                borderSide: new BorderSide(color: Colors.teal)),
                            hintText: 'Enter the discription',
                            labelText: 'Descriptions',
                            prefixIcon: const Icon(
                              Icons.description,
                              color: Colors.green,
                            )),
                        keyboardType: TextInputType.multiline,
                        maxLines: 5,
                      ),
                    ),

                    Material(
                      elevation: 5.0,
                      borderRadius: BorderRadius.circular(30.0),
                      color: Color(0xff01A0C7),
                      child: MaterialButton(
                        padding: EdgeInsets.fromLTRB(20.0, 15.0, 20.0, 15.0),
                        onPressed: () {
                          setState(() {
                            callIncidentTicket(
                                _incidentTicketValues
                                        .indexOf(_incidentTicketValue) +
                                    1,
                                _incidentLoggingValues
                                        .indexOf(_incidentLoggingValue) +
                                    1,
                                _incidentPrioritiesValues
                                        .indexOf(_incidentPrioritiesValue) +
                                    1,
                                _incidentCategoriesValues
                                        .indexOf(_incidentCategoriesValue) +
                                    1,
                                descriptionTextController.text);
                          });
                        },

                        ///action
                        child: Text(
                          "Submit",
                          textAlign: TextAlign.center,
                          style: TextStyle(
                            color: Colors.white,
                          ),
                        ),
                      ),
                    ),
                    SizedBox(
                      height: 20.0,
                    ),
                    //SimpleRecorder()
                  ],
                ),
              ),
              new Container(
                // padding: new EdgeInsets.only(top: (_height - 450.0)),
                margin: new EdgeInsets.only(bottom: 16.0),
                child: new FloatingActionButton(
                  backgroundColor: new Color(0xFFE57373),
                  child: new Icon(Icons.check),
                  onPressed: _launchURL, //() {
                  // Navigator.push(
                  //     context,
                  //     MaterialPageRoute(
                  //         builder: (context) => HtmlViewPage()));
                  //html.window.open(
                  //     'https://localhost:5001/api/IncidentManagements/gethtml',
                  //    'text/html');
                ),
              )
            ],
          ),
        ));
  }

  _launchURL() async {
    const url = 'https://localhost:5001/api/IncidentManagements/gethtml';
    if (await canLaunch(url)) {
      await launch(url,
          enableJavaScript: true,
          forceWebView: true,
          forceSafariVC: true,
          headers: <String, String>{'username': 'admin'});
    } else {
      throw 'Could not launch $url';
    }
  }
}
