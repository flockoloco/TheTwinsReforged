package com.example.mainactivity

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.app.AlertDialog
import androidx.recyclerview.widget.RecyclerView
import kotlinx.android.synthetic.main.items_layout.view.*
import kotlinx.android.synthetic.main.shopdialog.*

class ShopAdapter(var itemInv: List<Items>) : RecyclerView.Adapter<ShopAdapter.ShopViewHolder>() {

    inner class ShopViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ShopViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.items_layout, parent, false)
        return ShopViewHolder(view)
    }

    override fun getItemCount(): Int {
        return itemInv.size
    }

    override fun onBindViewHolder(holder: ShopViewHolder, position: Int) {
        holder.itemView.apply {
            itemTitle.text = itemInv[position].name
            itemDescription.text = itemInv[position].details
            itemIcon.setImageResource(itemInv[position].icon)
        }
        holder.itemView.setOnClickListener { view ->
            val dialogBox = LayoutInflater.from(view.context).inflate(R.layout.shopdialog, null)
            val builder = AlertDialog.Builder(view.context)
                .setView(dialogBox)
                .create()
            builder.show()

            //Starting config for the ores in the shop
            if (position == 0) {
                builder.shopTxt.text = "How many ores do you want to buy / sell ?"
                builder.shopImg.setImageResource(R.drawable.ic_gold_ingot)

                builder.shopClose.setOnClickListener {
                    builder.dismiss()
                }
                var count = 0
                builder.shopDecrease.setOnClickListener {
                    count--
                    if (count < 0) {
                        count = 0
                    }
                    builder.shopAmount.text = "$count"
                    builder.SellAmount.text = "$+$count"
                    builder.BuyAmount.text = "$-$count"
                }

                builder.shopIncrease.setOnClickListener {
                    count++
                    builder.shopAmount.text = "$count"
                    builder.SellAmount.text = "$+$count"
                    builder.BuyAmount.text = "$-$count"
                }

                builder.shopBuy.setOnClickListener {
                    if (Resources.Gold - count < 0) {
                        //do something in the future
                    } else {
                        Resources.Nuggets += count
                        Resources.Gold -= count
                        builder.dismiss()
                    }
                }
                builder.shopSell.setOnClickListener {
                    if (Resources.Nuggets - count < 0) {
                        //do something in the future
                    } else {
                        Resources.Nuggets -= count
                        Resources.Gold += count
                        builder.dismiss()
                    }
                }
            }
            //Starting config for the ingots in the shop
            if (position == 1) {
                builder.shopTxt.text = "How many ingots do you want to sell?"
                builder.shopImg.setImageResource(R.drawable.ic_ingot)

                builder.shopClose.setOnClickListener {
                    builder.dismiss()
                }
                var count = 0
                builder.shopDecrease.setOnClickListener {
                    count--
                    if (count < 0) {
                        count = 0
                    }
                    builder.shopAmount.text = "$count"
                    builder.SellAmount.text = "$+${count * 4}"
                }

                builder.shopIncrease.setOnClickListener {
                    count++
                    builder.shopAmount.text = "$count"
                    builder.SellAmount.text = "$+${count * 4}"
                }

                builder.shopSell.setOnClickListener {
                    if (count > Resources.Bars) {
                        //do something in the future
                    } else {
                        Resources.Bars -= count
                        Resources.Gold += (count * 4)
                        builder.dismiss()
                    }
                }

                builder.shopBuy.setBackgroundColor(R.color.gray_suit.toInt())
                builder.BuyAmount.textSize = 20f
                builder.BuyAmount.setTextColor(R.color.gray_suit.toInt())
                builder.shopBuy.isClickable = false
                builder.BuyAmount.text = "Not available!"
            }

            //Starting config for the Mine Speed in the shop
            if (position == 2) {
                builder.shopTxt.text = "How many Mine Speed upgrades do you want buy? 1 upgrade = 1 hour !"
                builder.shopImg.setImageResource(R.drawable.ic_upgrade)

                builder.shopClose.setOnClickListener {
                    builder.dismiss()
                }
                var count = 0
                builder.shopDecrease.setOnClickListener {
                    count--
                    if (count < 0) {
                        count = 0
                    }
                    builder.shopAmount.text = "$count"
                    builder.BuyAmount.text = "$-${count * 5}"
                }

                builder.shopIncrease.setOnClickListener {
                    count++
                    builder.shopAmount.text = "$count"
                    builder.BuyAmount.text = "$-${count * 5}"
                }

                builder.shopBuy.setOnClickListener {
                    if((Resources.Gold - count * 5) < 0){
                        //do something
                    }
                    else {
                        Resources.Minespd += count * 60
                        Resources.Gold -= (count * 5)
                        builder.dismiss()
                    }

                }
                builder.shopSell.setBackgroundColor(R.color.gray_suit.toInt())
                builder.SellAmount.textSize = 20f
                builder.SellAmount.setTextColor(R.color.gray_suit.toInt())
                builder.shopSell.isClickable = false
                builder.SellAmount.text = "Not available!"
            }

            //Starting config for the Mine Harvest in the shop
            if (position == 3) {
                builder.shopTxt.text = "How many Mine Harvest upgrades do you want buy? 1 upgrade = 1 hour !"
                builder.shopImg.setImageResource(R.drawable.ic_upgrade)

                builder.shopClose.setOnClickListener {
                    builder.dismiss()
                }
                var count = 0
                builder.shopDecrease.setOnClickListener {
                    count--
                    if (count < 0) {
                        count = 0
                    }
                    builder.shopAmount.text = "$count"
                    builder.BuyAmount.text = "$-${count * 5}"
                }

                builder.shopIncrease.setOnClickListener {
                    count++
                    builder.shopAmount.text = "$count"
                    builder.BuyAmount.text = "$-${count * 5}"
                }

                builder.shopBuy.setOnClickListener {
                    if((Resources.Gold - count * 5) < 0){
                        //do something
                    }
                    else{
                        Resources.MineHarvest += count * 60
                        Resources.Gold -= (count * 5)
                        builder.dismiss()
                    }

                }
                builder.shopSell.setBackgroundColor(R.color.gray_suit.toInt())
                builder.SellAmount.textSize = 20f
                builder.SellAmount.setTextColor(R.color.gray_suit.toInt())
                builder.shopSell.isClickable = false
                builder.SellAmount.text = "Not available!"
            }

            //Starting config for the Mine Ores in the shop
            if (position == 4) {
                builder.shopTxt.text = "How many Mine Ores upgrades do you want buy?"
                builder.shopImg.setImageResource(R.drawable.ic_upgrade)

                builder.shopClose.setOnClickListener {
                    builder.dismiss()
                }
                var count = 0
                builder.shopDecrease.setOnClickListener {
                    count--
                    if (count < 0) {
                        count = 0
                    }
                    builder.shopAmount.text = "$count"
                    builder.BuyAmount.text = "$-${count * 10}"
                }

                builder.shopIncrease.setOnClickListener {
                    count++
                    builder.shopAmount.text = "$count"
                    builder.BuyAmount.text = "$-${count * 10}"
                }

                builder.shopBuy.setOnClickListener {
                    if((Resources.Gold - count * 10) < 0){
                        // do something
                    }
                    else {
                        Resources.PermUpgrade += count
                        Resources.Gold -= (count * 10)
                        builder.dismiss()
                    }

                }
                builder.shopSell.setBackgroundColor(R.color.gray_suit.toInt())
                builder.SellAmount.textSize = 20f
                builder.SellAmount.setTextColor(R.color.gray_suit.toInt())
                builder.shopSell.isClickable = false
                builder.SellAmount.text = "Not available!"
            }
        }
    }
}