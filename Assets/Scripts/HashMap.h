#ifndef HASHMAP
#define HASHMAP

#include <iostream>
#include <vector>

template<typename K,typename V,typename Hash>
class HashMap {
    Hash hashFunction;
    unsigned int items;
    std::vector<std::vector<std::pair<K,V>>> table;
    // Data to store the hash table and the number of elements.

public:
    typedef K key_type;
    typedef V mapped_type;
    typedef std::pair<K,V> value_type;

    class const_iterator;

    class iterator {
        // NOTE: These might be different depending on how you store your table.
        decltype(table.begin()) mainIter;
        decltype(table.begin()) mainEnd;
        decltype(table[0].begin()) subIter;
    public:
        friend class const_iterator;

        // NOTE: These might be different depending on how you store your table.
        iterator(const decltype(mainIter) mi,const decltype(mainEnd) me):mainIter(mi),mainEnd(me) {
            if(mainIter!=mainEnd) subIter = mainIter->begin();
            while(mainIter!=mainEnd && subIter == mainIter->end()) {
                ++mainIter;
                if(mainIter!=mainEnd) subIter = mainIter->begin();
            }
        }
        // NOTE: These might be different depending on how you store your table.
        iterator(const decltype(mainIter) mi,
                const decltype(mainEnd) me,
                const decltype(subIter) si):
                mainIter(mi),mainEnd(me),subIter(si) {}

        bool operator==(const iterator &i) const { return mainIter==i.mainIter && (mainIter==mainEnd || subIter==i.subIter); }
        bool operator!=(const iterator &i) const { return !(*this==i); }
        std::pair<K,V> &operator*() { return *subIter; }
        iterator &operator++() {
            ++subIter;
            while(mainIter!=mainEnd && subIter==mainIter->end()) {
                ++mainIter;
                if(mainIter!=mainEnd) subIter = mainIter->begin();
            }
            return *this;
        }
        iterator operator++(int) {
            iterator tmp(*this);
            ++(*this);
            return tmp;
        }
    };

    class const_iterator {
        // NOTE: These might be different depending on how you store your table.
        decltype(table.cbegin()) mainIter;
        decltype(table.cbegin()) mainEnd;
        decltype(table[0].cbegin()) subIter;
    public:
        // NOTE: These might be different depending on how you store your table.
        const_iterator(const decltype(table.cbegin()) mi,const decltype(table.cbegin()) me):mainIter(mi),mainEnd(me) {
            if(mainIter!=mainEnd) subIter = mainIter->begin();
            while(mainIter!=mainEnd && subIter == mainIter->end()) {
                ++mainIter;
                if(mainIter!=mainEnd) subIter = mainIter->begin();
            }
        }
        // NOTE: These might be different depending on how you store your table.
        const_iterator(const decltype(table.begin()) mi,
            const decltype(table.begin()) me,
            const decltype(table[0].begin()) si):
                mainIter(mi),mainEnd(me),subIter(si) {}
        // NOTE: These might be different depending on how you store your table.
        const_iterator(const iterator &i):mainIter(i.mainIter),mainEnd(i.mainEnd),subIter(i.subIter) {

        }

        bool operator==(const const_iterator &i) const { return mainIter==i.mainIter && (mainIter==mainEnd || subIter==i.subIter); }
        bool operator!=(const const_iterator &i) const { return !(*this==i); }
        const std::pair<K,V> &operator*() const { return *subIter; }
        const_iterator &operator++() {
            ++subIter;
            while(mainIter!=mainEnd && subIter==mainIter->end()) {
                ++mainIter;
                if(mainIter!=mainEnd) subIter = mainIter->begin();
            }
            return *this;
        }
        const_iterator operator++(int) {
            const_iterator tmp(*this);
            ++(*this);
            return tmp;
        }
    };

    HashMap(const Hash &hf):hashFunction(hf),items(0),table(10){}

    //HashMap(const HashMap<K,V,Hash> &that); // Only if needed.

    //HashMap &operator=(const HashMap<K,V,Hash> &that); // Only if needed.

    bool empty() const {
      return (items == 0);
    }

    unsigned int size() const {
      return items;
    }

    iterator find(const key_type& k) {
      int bin = hashFunction(k)%(table.size());
      auto ret = table.begin()+bin;

      for(auto i = table[bin].begin(); i != table[bin].end(); ++i) {
        if(i -> first == k) {
          return iterator(ret, table.end(), i);
        }
      }
    return end();
    }


    const_iterator find(const key_type& k) const {
      int bin = hashFunction(k)%(table.size());
      auto ret = table.begin() + bin;

      for(auto i = table[bin].begin(); i != table[bin].end(); ++i) {
        if(i -> first == k) {
          return const_iterator(ret, table.end(), i);
        }
      }
    return cend();
    }

    std::pair<bool, iterator> fancyFind(const K &key) {
      auto bin = hashFunction(key) % table.size();
      for(auto iter = table[bin].begin(); iter != table[bin].end(); ++iter) {
        if((*iter).first == key) {
          return std::make_pair(true, iterator(table.begin() + bin, table.end(), iter));
        }
      }
      return std::make_pair(false, iterator(table.begin() + bin, table.end(), table[bin].end()));
    }

    int count(const key_type& k) const {
      int bin = hashFunction(k)%(table.size());
      for(auto i = table[bin].begin(); i != table[bin].end(); ++i) {
        if(i -> first == k) {
          return 1;
        }
      }
     return 0;
    }


    std::pair<iterator,bool> insert(const value_type& val) {

      if(items >= table.size()/2){
        growTableAndRehash();
      }
      int bin = hashFunction(val.first) % table.size();
      auto finder = fancyFind(val.first);
      if(finder.first) {
        return std::make_pair(finder.second, false);
      }
      else {
        //auto bin = hashFunction(val.first) % table.size();
        table[bin].push_back(val);
        ++items;
        return std::pair<iterator, bool> (fancyFind(val.first).second, true);
      }
    }

    template <class InputIterator>
    void insert(InputIterator first, InputIterator last) {
        // TODO: Put this one here to simplify the templates

    for(auto iter = first; iter != last; ++iter) {
        insert(*iter);
      }
    }

    iterator erase(const_iterator position) {
      int bin = hashFunction((*position).first)%(table.size());
      for(auto x = table[bin].begin(); x != table[bin].end();++x) {
        if(x->first == (*position).first) {
          table[bin].erase(x);
          --items;
          auto temp = ++position;
          return find((*temp).first);
         }
       }
    return find((*position).first);
    }

    int erase(const key_type& k) {
      int bin = hashFunction(k) % (table.size());
      for(auto i = table[bin].begin(); i != table[bin].end(); ++i){
        if(i->first==k) {
          table[bin].erase(i);
          --items;
          return 1;
        }
      }
    return 0;
    }

    void clear() {

      table.clear();
      table.resize(10);
      items = 0;
    }

    mapped_type &operator[](const K &key) {

    if(items >= table.size()/2) {
    growTableAndRehash();
    }

      int bin = hashFunction(key) % (table.size());
      mapped_type val;

      auto ff = fancyFind(key);
      if(ff.first) {
        return (*ff.second).second;
      } else {
        table[bin].push_back(std::make_pair(key, val));
        ++items;
        return (table[bin].end()-1)->second;
      }
    }


    bool operator==(const HashMap<K,V,Hash>& rhs) const {
      if(size() != rhs.size()) {
        return false;
      }

      auto y = rhs.begin();

      for(auto x = begin(); x != end(); ++x) {
        if(((*x).first != (*y).first) || ((*x).second != (*y).second)) {
          return false;
        }
        ++y;
      }
      if(y != rhs.end()) {
        return false;
      } else {
        return true;
      }
    }


    bool operator!=(const HashMap<K,V,Hash>& rhs) const {
      return !(*this == rhs);
    }

    iterator begin() {
      return iterator(table.begin(), table.end());
    }

    const_iterator begin() const {
      return const_iterator(table.begin(), table.end());
    }

    iterator end() {
      return iterator(table.end(), table.end());
    }

    const_iterator end() const {
      return const_iterator(table.end(), table.end());
    }

    const_iterator cbegin() const {
      return const_iterator(table.begin(), table.end());
    }

    const_iterator cend() const {
      return const_iterator(table.end(), table.end());
    }

    void printHash() {
      std::cout << "print Hash\n";
      for(unsigned int i = 0; i < table.size(); ++i) {
        std::cout << i << " : ";
        for(unsigned int j = 0; j < table[i].size(); ++j) {
          std::cout << "(" << table[i][j].first << "," << table[i][j].second << ")";
        }
        std::cout<<"\n";
      }
    }


private:
    void growTableAndRehash() {
     
      std::vector<std::vector<std::pair<K,V>>> tmp(table.size()*2+1);
      for(auto x = begin(); x != end(); ++x) {
        int bin = hashFunction((*x).first)%(tmp.size());
          tmp[bin].push_back(*x);
        }
      table = tmp;

    }

};

#endif
