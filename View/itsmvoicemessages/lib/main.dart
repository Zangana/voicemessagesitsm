import 'dart:io';
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:itsmvoicemessages/connectionpath.dart';
import 'package:itsmvoicemessages/indexpage.dart';
import 'responsivewidget.dart' as ResponsiveWidget;
import 'package:http/http.dart' as http;

class MyHttpOverrides extends HttpOverrides {
  // This class is for make connection with the localhost https://stackoverflow.com/questions/54285172/how-to-solve-flutter-certificate-verify-failed-error-while-performing-a-post-req.
  @override
  HttpClient createHttpClient(SecurityContext context) {
    return super.createHttpClient(context)
      ..badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
  }
}

void main() {
  HttpOverrides.global = new MyHttpOverrides(); // making the connection global.
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'ITSM Voice Messages',
      theme: ThemeData(
        // This is the theme of your application.
        //
        // Try running your application with "flutter run". You'll see the
        // application has a blue toolbar. Then, without quitting the app, try
        // changing the primarySwatch below to Colors.green and then invoke
        // "hot reload" (press "r" in the console where you ran "flutter run",
        // or simply save your changes to "hot reload" in a Flutter IDE).
        // Notice that the counter didn't reset back to zero; the application
        // is not restarted.
        primarySwatch: Colors.blue,
      ),
      home: MyHomePage(title: 'ITSM Voice Messages'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  MyHomePage({Key key, this.title}) : super(key: key);

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String title;

  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  TextStyle style = TextStyle(fontFamily: 'Montserrat', fontSize: 20.0);
  TextEditingController usernameTextController = new TextEditingController();
  TextEditingController passwordTextController = new TextEditingController();
  @override
  Widget build(BuildContext context) {
    // This method is rerun every time setState is called, for instance as done
    // by the _incrementCounter method above.
    //
    // The Flutter framework has been optimized to make rerunning build methods
    // fast, so that you can just rebuild anything that needs updating rather
    // than having to individually change instances of widgets.

    final usernameField = TextField(
      controller: usernameTextController,
      obscureText: false,
      style: style,
      decoration: InputDecoration(
        contentPadding: EdgeInsets.fromLTRB(20.0, 15.0, 20.0, 15.0),
        hintText: "UserName",
        border: OutlineInputBorder(borderRadius: BorderRadius.circular(32.0)),
      ),
    );
    final passwordField = TextField(
      controller: passwordTextController,
      obscureText: true,
      style: style,
      decoration: InputDecoration(
        contentPadding: EdgeInsets.fromLTRB(20.0, 15.0, 20.0, 15.0),
        hintText: "Password",
        border: OutlineInputBorder(borderRadius: BorderRadius.circular(32.0)),
      ),
    );
    final loginButton = Material(
      elevation: 5.0,
      borderRadius: BorderRadius.circular(30.0),
      color: Color(0xff01A0C7),
      child: MaterialButton(
          minWidth: MediaQuery.of(context).size.width,
          padding: EdgeInsets.fromLTRB(20.0, 15.0, 20.0, 15.0),
          onPressed: () {
            setState(() {
              getData(usernameTextController.text.toLowerCase(),
                  passwordTextController.text);
            });
          },
          child: Text(
            "Login",
            textAlign: TextAlign.center,
            style: style.copyWith(
                color: Colors.white, fontWeight: FontWeight.bold),
          )),
    );
    var screenSize = MediaQuery.of(context).size;
    return Scaffold(
      appBar: PreferredSize(
        preferredSize: Size(screenSize.width, 1000),
        child: Container(
          color: Colors.blue,
          child: Padding(
            padding: EdgeInsets.all(20),
            child: Row(
              children: [
                Text(
                  "ITSM Voice Messages",
                  style: TextStyle(
                      fontWeight: FontWeight.bold,
                      fontSize: 30,
                      color: Colors.white),
                ),
              ],
            ),
          ),
        ),
      ),
      body: Center(
          // Center is a layout widget. It takes a single child and positions it
          // in the middle of the parent.
          child: Container(
              color: Colors.white,
              child: Padding(
                  padding: const EdgeInsets.all(36.0),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.center,
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: <Widget>[
                      SizedBox(
                        height: 99.0,
                        //child: Image.asset("", fit: BoxFit.contain),
                      ),
                      SizedBox(height: 45.0),
                      usernameField,
                      SizedBox(height: 25.0),
                      passwordField,
                      SizedBox(height: 35.0),
                      loginButton,
                    ],
                  )))),
    );
  }

  /* 
     This method is for making connection with the Server.
     If the Server return 200 change the page to second page. 
     It takes two parameters which is username and password
     @param username, password.
  */
  Future getData(String username, String password) async {
    var params = {"username": username, "password": password};
    Map<String, String> headers = {
      // header for the http request.
      'Content-Type': 'application/json;charse=UTF-8',
      'charset': 'UTF-8'
    };
    final jsonString = json.encode(params); // encode the strings to json.
    var path = getPath();
    final response = await http.post(path + "Users/login",
        body: jsonString,
        headers:
            headers); // send the request to the server with the hearder and parameters.
    if (response.statusCode == 200) {
      // if it returns 200 change the page.
      Navigator.pushReplacement(
        // change the page without back.
        context,
        MaterialPageRoute(builder: (context) => IndexPage()), // change the page
      );
    } else {
      //else show a dialog box.
      return showDialog<void>(
          context: context,
          barrierDismissible: false,
          builder: (BuildContext context) {
            return AlertDialog(
              title: Text('FAILED TO LOGIN'),
              content: SingleChildScrollView(
                child: ListBody(
                  children: <Widget>[
                    Text('Please enter correct username or password'),
                  ],
                ),
              ),
              actions: <Widget>[
                TextButton(
                    onPressed: () {
                      Navigator.of(context).pop();
                    },
                    child: Text('OK'))
              ],
            );
          });
    }
  }
}
