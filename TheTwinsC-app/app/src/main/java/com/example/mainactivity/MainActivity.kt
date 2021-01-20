package com.example.mainactivity

import android.app.Dialog
import android.content.Context
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.util.AttributeSet
import android.util.Log
import android.view.Menu
import android.view.MenuItem
import android.view.View
import android.widget.LinearLayout
import androidx.appcompat.app.ActionBarDrawerToggle
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.DialogFragment
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.beust.klaxon.Klaxon
import com.example.mainactivity.retrofit.INodeJS
import com.example.mainactivity.retrofit.RetrofitClient
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.disposables.CompositeDisposable
import io.reactivex.schedulers.Schedulers
import kotlinx.android.synthetic.main.activity_main.*
import kotlinx.android.synthetic.main.drawer_header.view.*
import kotlinx.android.synthetic.main.email_dialog.*
import retrofit2.Retrofit


class MainActivity : AppCompatActivity() {

    lateinit var toggle: ActionBarDrawerToggle
    lateinit var myAPI: INodeJS
    var compositeDisposable = CompositeDisposable()

    val looper = Handler(Looper.getMainLooper())

    private lateinit var msgDialog: AlertDialog

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        //iniciar API
        val retrofit: Retrofit = RetrofitClient.instance
        myAPI = retrofit.create(INodeJS::class.java)

        msgDialog = AlertDialog.Builder(this)
            .setIcon(R.drawable.ic_heart)
            .setNeutralButton("Ok") { _, _ -> }
            .create()

        //Bottom navigation
        val anvilFragment = AnvilFragment()
        val mineFragment = MineFragment()

        setCurrentFragment(anvilFragment)

        bottom_navigation.setOnNavigationItemSelectedListener {
            when (it.itemId) {
                R.id.AnvilFragment -> setCurrentFragment(anvilFragment)
                R.id.MineFragment -> setCurrentFragment(mineFragment)
            }
            true
        }
        bottom_navigation.getOrCreateBadge(R.id.AnvilFragment).apply {
            number = Resources.Nuggets
            isVisible = true
        }
        //-=-=-=-=-=-=-=-=-=-=--=-=-=-=-==-=-=-=-=-=-=-=-==-=-=-=-==-=-=-=

        //Top navigation
        val shopFragment = ShopFragment()
        val inventoryFragment = InventoryFragment()

        toggle = ActionBarDrawerToggle(this, drawerLayout, R.string.open, R.string.close)
        drawerLayout.addDrawerListener(toggle)
        toggle.syncState()

        supportActionBar?.setDisplayHomeAsUpEnabled(true)

        val message = "Hi ${User.UserName}, welcome back"
        navigation_view.getHeaderView(0).txtmessage.text = message


        navigation_view.setNavigationItemSelectedListener {

            when (it.itemId) {
                R.id.ShopFragment -> setCurrentFragment(shopFragment)
                R.id.InventoryFragment -> setCurrentFragment(inventoryFragment)
                R.id.Quit -> onCloseActivity()
            }
            drawerLayout.closeDrawers()
            true
        }
        //-=-=-=-=-=-=-=-=-=-=--=-=-=-=-==-=-=-=-=-=-=-=-==-=-=-=-==-=-=-=

        looper.post(object : Runnable {
            override fun run() {
                functionUpdate()
                looper.postDelayed(this, 1500)
            }
        })

    }

    private fun functionUpdate() {
        bottom_navigation.getOrCreateBadge(R.id.AnvilFragment).apply {
            number = Resources.Nuggets
        }
    }

    private fun setCurrentFragment(fragment: Fragment) {
        supportFragmentManager.beginTransaction().apply {
            replace(R.id.hostFragment, fragment)
            addToBackStack(null)
            commit()
        }
    }

    override fun onCreateOptionsMenu(menu: Menu?): Boolean {
        menuInflater.inflate(R.menu.top_drawer, menu)

        return true
    }

    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        if (toggle.onOptionsItemSelected(item)) {
            return true
        }
        when (item.itemId) {
            R.id.emailcon -> {
                checkData()

            }
        }
        return super.onOptionsItemSelected(item)
    }

    override fun onStop() {
        compositeDisposable.clear()
        onCloseActivity()
        super.onStop()
    }

    override fun onResume() {
        bottom_navigation.getOrCreateBadge(R.id.AnvilFragment).apply {
            number = Resources.Nuggets
        }
        super.onResume()
    }

    private fun onCloseActivity() {
        compositeDisposable.add(myAPI.sendDB(
            Resources.Gold,
            Resources.Nuggets,
            Resources.Bars,
            Resources.Minespd,
            Resources.MineHarvest,
            Resources.PermUpgrade,
            Resources.FirstTime,
            User.UserID
        )
            .subscribeOn(Schedulers.io())
            .observeOn(AndroidSchedulers.mainThread())
            .subscribe {}
        )
        finish()
    }

    override fun onDestroy() {
        compositeDisposable.clear()
        super.onDestroy()
    }

    fun checkData() {
        compositeDisposable.add(
            myAPI.checkDelivery(User.UserID, 1)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe { message ->
                        val result = Klaxon().parse<MailBoxClass>(message)
                        MailBox.Ores = result!!.OresAmount
                        MailBox.Bars = result.BarsAmount
                        var dialog = EmailDialog()
                        dialog.show(supportFragmentManager, "customDialog")
                }
        )
    }
}