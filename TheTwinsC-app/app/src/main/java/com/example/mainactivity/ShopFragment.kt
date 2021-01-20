package com.example.mainactivity

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.View
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView

class ShopFragment : Fragment(R.layout.fragment_shop) {
    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        var shopList = mutableListOf(
            Items("Ore", "Ores are gathered every hour inside the mine", R.drawable.ic_gold_ingot),
            Items("Ingot", "Ingots are forged in the anvil, it can be traded in the shop or sent to the player", R.drawable.ic_ingot),
            Items("Mine Speed", "Temporary speed upgrade, the ores are gathered every 1/2 hour", R.drawable.ic_upgrade),
            Items("Mine Harvest", "Temporary harvest upgrade, the amount of ores gathered are x2", R.drawable.ic_upgrade),
            Items("Mine Ores", "Permanent ores upgrade, the amount of ores gathered are +1", R.drawable.ic_upgrade)
        )

        val shopRecycler = view.findViewById<RecyclerView>(R.id.shopRecycler)
        shopRecycler.adapter = ShopAdapter(shopList)
        shopRecycler.layoutManager = LinearLayoutManager(this.activity)
    }
}