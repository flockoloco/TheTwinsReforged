package com.example.mainactivity

import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.util.Log
import android.view.View
import androidx.fragment.app.Fragment
import kotlinx.android.synthetic.main.fragment_anvil.*
import kotlinx.android.synthetic.main.fragment_mine.*

class MineFragment : Fragment(R.layout.fragment_mine) {

    val looper = Handler(Looper.getMainLooper())
    var time: Long = 0
    var txtTime: String = ""

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        //do as same in the onResume but once the player opens it
        UPtoDate()
        MineHarvest.max = Resources.MineHarvest
        MineSPD.max = Resources.Minespd

        //a simple loop so the player gets ores after some TIME / i miss the Update function =(
        looper.post(object : Runnable {
            override fun run() {
                if (getView() != null) {
                    miningOre()
                    if (Resources.Minespd == 0) {
                        time = 1000
                        txtTime = "per hour"
                    } else {
                        time = 2000
                        txtTime = "per 30 minutes"
                        MineSPD.incrementProgressBy(-1)
                        Resources.Minespd -= 1
                    }
                }
                looper.postDelayed(this, time)
                //Put 60 000 without upgrade or 30 000 with upgrade,  leave 2000 (1 seg) for test purposes or 1000 (1 seg) with upgrade
            }
        })

    }

    private fun miningOre() {
        //decrease the time and increase the ore (working with the upgrades)
        if (OrePB.progress == 0) {
            OrePB.progress = 100
        }

        OrePB.incrementProgressBy(-1)

        if (MineHarvest.progress != 0) {
            Resources.Nuggets += Resources.PermUpgrade * 2
            MineHarvest.incrementProgressBy(-1)
            Resources.MineHarvest -= 1
        } else {
            Resources.Nuggets += Resources.PermUpgrade
        }
    }

    override fun onResume() {
        //reload bar UI so the max progress and the progress is correct
        UPtoDate()
        MineHarvest.max = Resources.MineHarvest
        MineSPD.max = Resources.Minespd
        super.onResume()
    }

    private fun UPtoDate(){
        OrePB.progress = 100
        MineHarvest.progress = Resources.MineHarvest
        MineSPD.progress = Resources.Minespd


        OrePBtxt.text = "Mine: +${Resources.PermUpgrade} ores $txtTime"
        MineSpdtxt.text = "Mine speed upgrade: ${String.format("%.1f" ,(Resources.Minespd.toDouble() / 60))} hours left"
        MineHarvesttxt.text = "Mine harvest upgrade: ${String.format("%.1f" ,(Resources.MineHarvest.toDouble() / 60))} hours left"

    }
}