package com.example.mainactivity

import android.content.Intent
import android.os.Bundle
import android.text.TextUtils
import android.util.Log
import android.view.LayoutInflater
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity
import com.beust.klaxon.Json
import com.beust.klaxon.JsonObject
import com.beust.klaxon.JsonReader
import com.beust.klaxon.Klaxon
import com.example.mainactivity.retrofit.INodeJS
import com.example.mainactivity.retrofit.RetrofitClient
import com.google.gson.Gson
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.disposables.CompositeDisposable
import io.reactivex.schedulers.Schedulers
import kotlinx.android.synthetic.main.activity_login.*
import kotlinx.android.synthetic.main.confirmpassword_box.view.*
import org.json.JSONStringer
import retrofit2.Retrofit
import java.io.Serializable
import java.io.StringReader
import kotlin.Error
import kotlin.reflect.jvm.internal.impl.load.kotlin.JvmType

class LoginActivity : AppCompatActivity() {

    lateinit var myAPI: INodeJS
    var compositeDisposable = CompositeDisposable()
    private lateinit var msgDialog: AlertDialog

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)

        msgDialog = AlertDialog.Builder(this)
            .setIcon(R.drawable.ic_error)
            .setTitle("Error")
            .setMessage("Wrong Username or Password, please try again!")
            .setNeutralButton("Ok") { _, _ -> }
            .create()

        //iniciar API
        val retrofit: Retrofit = RetrofitClient.instance
        myAPI = retrofit.create(INodeJS::class.java)

        btnLogin.setOnClickListener {
            val username = lgnUsername.text.toString()
            val password = lgnPassword.text.toString()
            if (!fieldsCheck()) {
                login(username, password)
            }
        }

        btnRegister.setOnClickListener {
            val username = lgnUsername.text.toString()
            val password = lgnPassword.text.toString()
            if (!fieldsCheck()) {
                register(username, password)
            }
        }
    }

    private fun login(Username: String, Password: String) {
        compositeDisposable.add(myAPI.loginUser(Username, Password)
            .subscribeOn(Schedulers.io())
            .observeOn(AndroidSchedulers.mainThread())
            .subscribe { message ->
                if (message.contains("UserID")) {
                    val result = Klaxon().parse<UserClass>(message)
                    if (result != null) {
                        User.UserName = result.UserName
                        User.UserID = result.UserID
                        identifyUser(result.UserID)
                    }
                }
                if (message.contains("Status")) {
                    val result = Klaxon().parse<ErrorStatus>(message)
                    if (result!!.Status == 1) {
                        msgDialog.setMessage("Wrong password, please try it again!")
                        msgDialog.show()
                    }
                    if (result.Status == 2) {
                        msgDialog.setMessage("Account does not exist, please create one!")
                        msgDialog.show()
                    }
                }
            }
        )
    }

    private fun register(Username: String, Password: String) {
        compositeDisposable.add(myAPI.loginUser(Username, Password)
            .subscribeOn(Schedulers.io())
            .observeOn(AndroidSchedulers.mainThread())
            .subscribe { message ->
                if (message.contains("Status")) {
                    val result = Klaxon().parse<ErrorStatus>(message)
                    if (result!!.Status == 1) {
                        msgDialog.setMessage("User already exists, please insert a different username!")
                        msgDialog.show()
                    }
                    else{
                        val inflatePassword =
                            LayoutInflater.from(this).inflate(R.layout.confirmpassword_box, null)

                        AlertDialog.Builder(this)
                            .setTitle("Confirm Password")
                            .setView(inflatePassword)
                            .setNegativeButton("Cancel") { _, _ -> }
                            .setPositiveButton("Confirm") { _, _ ->
                                val confirmedPass = inflatePassword.lgnConfirmPassword as EditText
                                if (Password == confirmedPass.text.toString()) {
                                    compositeDisposable.add(myAPI.registerUser(Username, Password)
                                        .subscribeOn(Schedulers.io())
                                        .observeOn(AndroidSchedulers.mainThread())
                                        .subscribe { _ ->
                                            Toast.makeText(
                                                this,
                                                "successfully register",
                                                Toast.LENGTH_SHORT
                                            ).show()
                                        }
                                    )
                                } else {
                                    msgDialog.setMessage("Password does not match, please try again! ")
                                    msgDialog.show()
                                }
                            }.show()
                    }
                }
            }
        )
    }


    private fun fieldsCheck(): Boolean {
        if (TextUtils.isEmpty(lgnUsername.text.toString()) || TextUtils.isEmpty(lgnPassword.text.toString())) {
            if (TextUtils.isEmpty(lgnUsername.text.toString())) {
                lgnUsername.error = "Please insert a username!"
            }
            if (TextUtils.isEmpty(lgnPassword.text.toString())) {
                lgnPassword.error = "Please insert a password!"
            }
            return true
        }
        return false
    }

    private fun identifyUser(UserID: Int) {
        compositeDisposable.add(myAPI.getApp(UserID)
            .subscribeOn(Schedulers.io())
            .observeOn(AndroidSchedulers.mainThread())
            .subscribe { message ->
                Intent(this, MainActivity::class.java).also {
                    val result = Klaxon().parse<ResourcesClass>(message)
                    if (result != null) {
                        Resources.Gold = result.Gold
                        Resources.Nuggets = result.Nuggets
                        Resources.Bars = result.Bars
                        Resources.Minespd = result.MineSpd
                        Resources.MineHarvest = result.MineHarvest
                        Resources.PermUpgrade = result.PermUpgrade
                        Resources.FirstTime = result.FirstTime
                    }
                    startActivity(it)
                }

                //3 delivery tutorial REMEMBER
                /*if (Resources.FirstTime == -1) {
                    msgDialog.setTitle("Welcome!")
                    msgDialog.setMessage(
                        "Hi ${User.UserName}, welcome to The Twins companion app, here you can manage resources and send them to the main game if needed. " +
                                "for your first login we will give you ${Resources.Gold} Gold, ${Resources.Nuggets} Nuggets, " +
                                "Hope you enjoy! :) "
                    )
                    msgDialog.show()
                    Resources.FirstTime = 0
                }*/
            }
        )
    }

    override fun onStop() {
        compositeDisposable.clear()
        super.onStop()
    }

    override fun onDestroy() {
        compositeDisposable.clear()
        super.onDestroy()
    }
}
