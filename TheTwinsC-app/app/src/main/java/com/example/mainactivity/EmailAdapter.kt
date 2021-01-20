package com.example.mainactivity

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import kotlinx.android.synthetic.main.items_layout.view.*

class EmailAdapter(var itemEmail: List<Items>): RecyclerView.Adapter<EmailAdapter.EmailViewHolder>() {

    inner class EmailViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): EmailViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.items_layout, parent, false)
        return EmailViewHolder(view)
    }

    override fun onBindViewHolder(holder: EmailViewHolder, position: Int) {
        holder.itemView.apply {
            itemTitle.text = itemEmail[position].name
            itemDescription.text = itemEmail[position].details
            itemIcon.setImageResource(itemEmail[position].icon)
        }
    }

    override fun getItemCount(): Int {
        return itemEmail.size
    }
}