import 'package:http/http.dart' as http;
import 'dart:convert';
import 'dart:async';

import 'package:itsmvoicemessages/connectionpath.dart';

Future<User> fetchUser(String username, String password) async {
  final http.Response response =
      await http.post(getPath() + 'Users/login', headers: <String, String>{
    'Accept': 'application/json',
  }, body: {
    'username': username,
    'password': password,
  });
  if (response.statusCode == 200) {
    return User.fromJson(jsonDecode(response.body));
  } else {
    throw Exception(response.toString());
  }
}

class User {
  final int userId;
  final String userName;
  final String password;
  final String userType;
  final int active;
  final DateTime dateCreated;

  User(
      {this.userId,
      this.userName,
      this.password,
      this.userType,
      this.active,
      this.dateCreated});

  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      userId: json['userId'],
      userName: json['userName'],
      password: json['password'],
    );
  }
}
