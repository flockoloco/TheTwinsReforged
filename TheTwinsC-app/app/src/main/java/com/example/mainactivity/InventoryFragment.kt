package com.example.mainactivity

import android.content.Intent
import android.graphics.Color
import android.graphics.drawable.ColorDrawable
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.os.PersistableBundle
import android.util.Log
import android.view.LayoutInflater
import androidx.fragment.app.Fragment
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.app.AppCompatActivity
import androidx.core.app.SharedElementCallback
import androidx.fragment.app.FragmentTransaction
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import kotlinx.android.synthetic.main.activity_main.*
import kotlinx.android.synthetic.main.fragment_mine.*

class InventoryFragment : Fragment(R.layout.fragment_inventory) {

    val looper = Handler(Looper.getMainLooper())

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        looper.post(object : Runnable {
            override fun run() {
                if (getView() != null) {
                    fillList()
                }
                looper.postDelayed(this, 2000)
            }
        })
    }

    fun fillList() {
        val invList = mutableListOf(
            Items(
                "Coin x${Resources.Gold}",
                "Coins are the currency trading of this game!",
                R.drawable.ic_money
            ),
            Items(
                "Ore x${Resources.Nuggets}",
                "Ores are gathered every hour inside the mine",
                R.drawable.ic_gold_ingot
            ),
            Items(
                "Ingot x${Resources.Bars}",
                "Ingots are forged in the anvil, it can be traded in the shop or sent to the player",
                R.drawable.ic_ingot
            ),
            Items(
                "Mine Speed Upgrade",
                "approximately ${
                    String.format(
                        "%.1f",
                        (Resources.Minespd.toDouble() / 60)
                    )
                }/h left, the ores are gathered every hour",
                R.drawable.ic_upgrade
            ),
            Items(
                "Mine Harvest Upgrade",
                "approximately ${
                    String.format(
                        "%.1f",
                        (Resources.MineHarvest.toDouble() / 60)
                    )
                }/h left, the amount of ores gathered are x2",
                R.drawable.ic_upgrade
            ),
            Items(
                "Mine Ores Upgrade",
                "Permanent ores upgrade, +${Resources.PermUpgrade} ores per hour",
                R.drawable.ic_upgrade
            )
        )

        val invRecycler = view?.findViewById<RecyclerView>(R.id.invRecycler)
        if (invRecycler != null) {
            invRecycler.adapter = InvAdapter(invList)
            invRecycler.layoutManager = LinearLayoutManager(this.activity)
        }
    }
}